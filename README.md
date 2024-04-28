# PageControl for WinForms

`PageControl`是一个为Windows Forms应用程序提供的自定义用户控件，用于实现数据分页显示功能。该控件允许用户通过简单的界面导航不同的数据页，包括首页、上一页、下一页和尾页功能，以及直接跳转到特定页码。

## 功能

- **基本分页**：浏览数据的首页、上一页、下一页和尾页。
- **直接跳转**：输入页码直接跳转到指定页面。
- **动态设置页大小**：允许用户动态设置每页显示的记录数。
- **自定义页数**：用户可以输入希望的总页数，从而间接设置页大小。

## 安装使用



## 示例代码

## API
 **属性**
-**int PageSize** - 获取或设置每页显示的记录数。
-**DataGridView** - 获取或设置控件绑定的DataGridView。
 **方法**
-**void SetDataSource(DataTable dataTable)** - 设置控件的数据源。
-**void NavigateTo(int page)** - 导航到指定的页码。


