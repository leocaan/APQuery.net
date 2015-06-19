APQuery.net
=======

APQuery.net is a lightweight ORM, open source framework. With the objectization of the database, 
SQL expression, database access layout, business logic process and data entity.

**<font size="+1">Home page, documentation, and support links: http://leocaan.github.io/APQuery.net/</font>**

				To get started, checkout examples and documentation at
				https://select2.github.io/

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

```
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

Usage of ORM
------------

1. Insert a data
```
var dep = new Department(1, 0, "Sales", "000-000-0000");
dep.Insert();
```

2. Delete a data
```
Department.PrimaryDelete(1);
```

3. Get data with primary key and update
```
var dep = Department.PrimaryGet(1);
dep.Phone = "000-000-0001";
dep.Update();
```

3. Partial update
```
de
```

2. Delete a data
```
```


Copyright and license
---------------------
Copyright (c) 2014-2015 Leo Caan. The license see [LICENSE][license] file.



[license]: LICENSE.md
