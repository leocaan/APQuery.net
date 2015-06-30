APQuery.net
=======

APQuery.net is a lightweight ORM, open source framework. With the objectization of the database, 
SQL expression, database access layout, business logic process and data entity.

**<font size="+1">Home page, documentation, and support links: http://leocaan.github.io/APQuery.net/</font>**

**<font size="+1">Some demos will be links: https://github.com/leocaan/APQuery.net-Demo/</font>**


Getting Started
---------------

* Install **APQuery.net Library Package** from **Nuget**, search as the key "APQuery".
* Install **APQuery.net Add-in** from **Expanded and updated**, 
  Visual Studio Menu -> Tools -> Expanded and updated, search as the key "APQuery".

[![](http://i57.tinypic.com/16iv2j9.jpg)](http://i57.tinypic.com/16iv2j9.jpg)

* Right click contextmenu on the project folder and click **Add new .apgen file**
  to create a **business.apgen** file, this file is the ORM mapping file.

[![](http://i58.tinypic.com/n32qv5.jpg)](http://i58.tinypic.com/n32qv5.jpg)

* If you project is WebSite, ensure the file is in the **App_Code** folder will be auto generated.
* Or if you project is WebApplication, right click context menu on the file and click
  **Generate the .apgen file** item will be generated **business.apgen.cs** in project.

[![](http://i59.tinypic.com/2638nmh.jpg)](http://i59.tinypic.com/2638nmh.jpg)

* Edit **Global.asax**, add **Symber.Web.Compilation.APGenManager.SyncAndInitData();** in the
  **Application_Start** method, that will be automatically create and maintain the DATABASE and
  initialization data.


About the .agpen file
---------------------

  Look at here simple, the details please refer to the documents.

```xml
<businessModel autoSyncDatabase="true" autoInitDatabase="true">
  <tables>
    <table name="Department" comment="Departments of company">
      <columns>
        <add name="DepartmentId" type="int" primaryKey="true" identityType="Provider"/>
        <add name="ParentId" type="int" comment="Parent DepartmentId"/>
        <add name="DeptName" type="string" dataLength="20"/>
        <add name="Phone" type="string" dataLength="20"/>
      </columns>
      <uniques>
        <index name="IX_Department_DeptName">
          <add name="DeptName" according="Asc"/>
        </index>
      </uniques>
      <aliases>
        <add name="Parent"/>
      </aliases>
    </table>

    <table name="Employee" comment="Employees of company">
      <columns>
        <add name="EmployeeId" type="int" primaryKey="true" identityType="Provider"/>
        <add name="DepartmentId" type="int" comment="Employee's DeparentId"/>
        <add name="Name" type="string" dataLength="30"/>
        <add name="Birthday" type="DateTime"/>
        <add name="Email" type="string" dataLength="255"/>
      </columns>
      <indexes>
        <index name="IX_Employee_Name" isDefault="true">
          <add name="Name" according="Asc"/>
        </index>
      </indexes>
    </table>			 
  </tables>
</businessModel>
```


What objectizations does we have
--------------------------------

```
---------------------------------------------------------------------
Entity Defined                          |   Department
---------------------------------------------------------------------
Database Struct Defined                 |   APDBDef.Department
---------------------------------------------------------------------
Data Access Layout (Dal) Defined        |   APDalDef.DepartmentDal
---------------------------------------------------------------------
Business Process Logic (Bpl) Defined    |   APBplDef.DepartmentBpl
---------------------------------------------------------------------
```


Usage of ORM
------------

**Insert a data**
```cs
var dep = new Department(1, 0, "Sales", "000-000-0000");
dep.Insert();
```

**Delete a data**
```cs
Department.PrimaryDelete(1);
```

**Condition delete**
```cs
// short refer name
var t = APDBDef.Department;
Department.ConditionDelete(t.ParentId == 0 & t.DeptName != "Sales");
```

**Get data with primary key and update**
```cs
var dep = Department.PrimaryGet(1);
dep.Phone = "000-000-0001";
dep.Update();
```

**Partial update**
```cs
Department.UpdatePartial(1, new { Phone="000-000-5555", DeptName="New Seals"});
```

**Condition query and order**
```cs
var t = APDBDef.Department;
List<Department> list = Department.ConditionQuery(
   t.ParentId == 0 & t.DeptName != "Sales",  // condition
   t.DepartmentId.Desc);                     // order
```

**Paging query**
```cs
var t = APDBDef.Department;
List<Department> list = Department.ConditionQuery(
   t.ParentId == 0 & t.DeptName != "Sales",  // condition
   t.DepartmentId.Desc,                      // order
   20, 20);       // take 20 records, skip ahead 20 records
```

**Query count**
```cs
Department.ConditionQueryCount(t.ParentId != 0);
```

**Transcation and Dal**
```cs
using (APDBDef db = new APDBDef())
{
   db.BeginTrans();

   try
   {
      db.DepartmentDal.PrimaryDelete(1);
      db.EmployeeDal.ConditionDelete(APDBDef.Employee.DepartmentId == 1);

      db.Commit();
   }
   catch
   {
      db.Rollback();
   }
}
```


Usage of SQL Expression
-----------------------
Sometimes the ORM can't meet our requirement, so we can directly use SQL Expression.
Of course, the core of ORM is also dependent on SQL Expression.

**Asterisk Query**
```cs
var dep = APDBDef.Department;
using (APDBDef db = new APDBDef())
{
   IEnumerable<Department> result = APQuery
      .select(dep.Asterisk).distinct()
      .from(dep)
      .where(dep.ParentId == 0 & dep.Phone != null)
      .order(dep.DeptName.Desc)
      .query(db, dep.Map);
}
```
Execute on SQLServer provider.
```sql
SELECT DISTINCT Department.* 
  FROM Department
  WHERE Department.ParentId = 0 AND Department.Phone IS NOT NULL
  ORDER BY Department.DeptName DESC
```

In the following, we only write SQL Expression.

**Column Alias**
```cs
APQuery
   .select(dep.DepartmentId, dep.DeptName.As("Name"), dep.Phone.As("Dept Phone"))
   .from(dep);
```
```sql
SELECT Department.DepartmentId, Department.DeptName AS Name,
       Department.Phone AS [Dept Phone]
  FROM Department
```

**Multi-table query**
```cs
var d = APDBDef.Department;
var e = APDBDef.Employee;
APQuery
   .select(e.EmployeeId, d.DeptName, e.Name)
   .from(d, e.JoinInner(d.DepartmentId == e.DepartmentId))
   .where(d.ParentId.NotIn(2, 3, 4));
```
```sql
SELECT Employee.EmployeeId, Department.DeptName, Employee.Name
  FROM Department
  INNER JOIN Employee ON Department.DepartmentId == Employee.DepartmentId
  WHERE Department.DepartmentId IN ( 2, 3, 4 )
```

**Alias table query**
```cs
var d = APDBDef.Department;
var dp = APDBDef.Department.Parent;
APQuery
   .select(d.DepartmentId, d.DeptName, dp.DeptName.As ("ParentName"))
   .from(d, dp.JoinLeft(d.ParentId == dp.DepartmentId))
   .where(dp.DeptName.Match("ale"));
```
```sql
SELECT Department.DepartmentId, Department.DeptName, Parent.DeptName AS 'ParentName'
  FROM Department
  LEFT JOIN Department AS Parent ON Department.ParentId == Parent.DepartmentId
  WHERE Parent.DeptName LIKE '%ale%'
```

**Subquery**
```cs
var d = APDBDef.Department;
var subQuery = APQuery
   .select(d.DepartmentId)
   .from(d)
   .where(d.ParentId == 0);
APQuery
   .select(d.Asterisk)
   .from(d)
   .where(subQuery.exist());
```
```sql
SELECT Department.*
  FROM Department
  WHERE ( EXISTS (
    SELECT Department.DepartmentId
      FROM Department
		WHERE Department.ParentId = 0
  ) )
```

**Paging Query**
```cs
var d = APDBDef.Department;
var e = APDBDef.Employee;
var query = APQuery
   .select(e.EmployeeId, e.Name, d.DeptName)
   .from(e, d.JoinInner(e.DepartmentId == d.DepartmentId))
   .primary(e.EmployeeId)
   .take(20)
   .skip(20);

using (APDBDef db = new APDBDef())
{
   int total = db.ExecuteSizeOfSelect(query);
   IDataReader records = db.ExecuteReader(query);
}
```
Execute on SQLServer provider.
```sql
SELECT COUNT(*)
  FROM Employee,
    INNER JOIN Department ON Employee.DepartmentId = Department.DepartmentId

SELECT TOP 20 Employee.EmployeeId, Employee.Name, Department.DeptName
  FROM Employee,
    INNER JOIN Department ON Employee.DepartmentId = Department.DepartmentId
  WHERE Employee.EmployeeId NOT IN (
    SELECT TOP 20 Employee.EmployeeId
	   FROM Employee
		  INNER JOIN Department ON Employee.DepartmentId = Department.DepartmentId
  )
```
Execute on Oracle provider.
```sql
SELECT COUNT(*)
  FROM Employee,
    INNER JOIN Department ON Employee.DepartmentId = Department.DepartmentId

SELECT * FROM ( SELECT query_alias.*, ROWNUM query_rownum FROM (
  SELECT Employee.EmployeeId, Employee.Name, Department.DeptName
    FROM Employee,
      INNER JOIN Department ON Employee.DepartmentId = Department.DepartmentId
  ) query_alias WHERE ROWNUM <= 40 ) WHERE query_rownum > 20)
```

**Aggregation & Group By**
```cs
var t = APDBDef.Department;
var e = APDBDef.Employee;
APQuery
   .select(t.DepartmentId, t.DeptName, e.EmployeeId.Count())
   .from(t, e.JoinLeft(t.DepartmentId == e.DepartmentId))
   .group_by(t.DepartmentId, t.DeptName, e.EmployeeId)
   .having(e.EmployeeId.Count() > 0);
```
```sql
SELECT Department.DepartmentId, Department.DeptName, COUNT(Employee.EmployeeId)
FROM Department
     LEFT JOIN Employee ON Department.DepartmentId = Employee.DepartmentId
GROUP BY Department.DepartmentId, Department.DeptName, Employee.EmployeeId
HAVING COUNT(Employee.EmployeeId) > 0
```

**Aggregation with Date**
```cs
var t = APDBDef.Employee;
APQuery
   .select(t.Birthday.DateGroup(APSqlDateGroupMode.Month),
	   new APSqlAggregationExpr(t.Birthday.DateGroup(APSqlDateGroupMode.Month), APSqlAggregationType.COUNT)))
   .from(t)
   .group_by(t.Birthday.DateGroup(APSqlDateGroupMode.Month));
```
Execute on SQLServer provider.
```sql
SELECT DATEADD( mm, DATEDIFF( mm, 0, Employee.Birthday ), 0 ),
   COUNT( DATEADD( mm, DATEDIFF( mm, 0, Employee.Birthday ), 0 ) )
   FROM Employee
   GROUP BY DATEADD( mm, DATEDIFF( mm, 0, Employee.Birthday ), 0 )
```
Execute on Oracle provider.
```sql
SELECT to_char(Employee.Birthday, 'yyyy-mm' ),
   COUNT( to_char(Employee.Birthday, 'yyyy-mm' ) )
   FROM Employee
   GROUP BY to_char(Employee.Birthday, 'yyyy-mm' )
```


**Insert**
```cs
var d = APDBDef.Department;
APQuery
   .insert(d)
      .set(d.DepartmentId, 5)
      .set(d.DeptName, "HR")
      .set(d.Phone, "000-111-2222");
```
```sql
INSERT INTO Department
  (Department.DepartmentId, Department.DeptName, Department.Phone)
  Values (5, 'HR', '000-111-2222')
```

**Update**
```cs
var d = APDBDef.Department;
APQuery
   .update(d)
      .set(d.Phone, "000-111-3333")
   .where(d.DepartmentId == 5);
```
```sql
UPDATE Department
    SET Department.Phone = '000-111-3333'
  WHERE Department.DepartmentId = 5
```

**Delete**
```cs
var d = APDBDef.Department;
APQuery
   .delete(d)
   .where(d.ParentId == 0);
```
```sql
DELETE Department
  WHERE Department.ParentId = 0
```

**Anonymous return**
```cs
var d = APDBDef.Department;
var query = APQuery
   .select(d.DepartmentId, d.DeptName)
   .from(d);

using (APDBDef db = new APDBDef())
{
   var records = db.Query(query, r =>
   {
      return new
      {
         id = d.DepartmentId.GetValue(r),
         name = d.DeptName.GetValue(r)
      };
   });
}
```



Copyright and license
---------------------
Copyright (c) 2014-2015 Leo Caan. The license see [LICENSE][license] file.



[license]: LICENSE.md
