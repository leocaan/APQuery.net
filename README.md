APQuery.net
=======

APQuery.net is a lightweight ORM, open source framework. With the objectization of the database, 
SQL expression, database access layout, business logic process and data entity.

**<font size="+1">Home page, documentation, and support links: http://leocaan.github.io/APQuery.net/</font>**


Getting Started
---------------

* Create and edit **business.apgen** file, this file is the ORM mapping file.
* If you project is WebSite, ensure the file is in the **App_Code** folder will be auto generated.
* Or if you project is WebApplication, right click context menu on the file and click **APGen Generate**
  item will be generated **business.apgen.cs** in project.
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


Usage of SQL Expression
-----------------------
Sometimes the ORM can't meet our requirement, so we can directly use SQL Expression.
Of course, the core of ORM is also dependent on SQL Expression.

**Simple 'SELECT'**
```cs
var dep = APDBDef.Department;
using (APDBDef db = new APDBDef())
{
   IEnumerable<Department> result = APQuery
      .select(dep.Asterisk)
      .from(dep)
      .where(dep.ParentId == 0)
      .query(db, dep.Map);
}
```
```sql
SELECT Department.* 
  FROM Department
  WHERE Department.ParentId = 0
```

In the following, we only write SQL Expression.



Copyright and license
---------------------
Copyright (c) 2014-2015 Leo Caan. The license see [LICENSE][license] file.



[license]: LICENSE.md
