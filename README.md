# DocsAPI
RESTfull API to manage file uploading & downloading

Purpose:
We all need document/files uploading in our applications. In this repository, we are creating a standard RESTFull Service (using ASP.NET WEB API) which will allow you to upload your documents and will give you unique IDs. You then can manage those IDs in your own DB. Later you may use those IDs and download the document from API.

Repo contains:
- DocsAPI project (ASP.NET Web API)
- DocsAPITestConsumer Project (ASP.NET MVC Tester project which is consuming DocsAPI)
- DBScripts folder (This contains schema of database being used by Web API
- DataFramework folder (This contains DAL & Entities Project)
- Common folder (this contains common utility projects)

oAuth Support:
- DocsAPI contains oAuth based token generation

How to Start:
- You can ASP.NET Web API video series if you have any question or confusion regarding ASP.NET Web API or token generation or how to consume token
- Check "DocsAPITestConsumer\BaseController.cs" file
- Check "DocsAPITestConsumer\Models\TokenHelper.cs" file
- Check "DocsAPITestConsumer\Views\Home\Index.cshtml"

- Right click on solution file in "Solution Explorer", Go to Properties and then choose "Multiple" and then set "Start" for "DocsAPI" & for "DocsAPITestConsumer"

- Open "DocsAPI\web.config" file and update connection string

- Rebuild the solution and run by F5

- You will see different links for your testing on "http://localhost:2833/Home/Index".

- Note: In "index.cshtml" page, ID of a file is hardcoded. You may upload few files and then update this ID with newly generated ID (you may get it from DB)
