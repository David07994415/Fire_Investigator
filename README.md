 <p align="center">
    <img width="1200" src ="https://raw.githubusercontent.com/David07994415/Fire_Investigator/main/WebApplication1/ReadmeCover.png">
</p>
 
<h1 align="center" style="font-weight: 700"><img alt="MVC5" src="https://img.shields.io/badge/.Net_Framework-MVC_5-Green"> 消防協會組織網站  | .Net Framework MVC 5 </h1>
<div align="center" style="margin-bottom:24px">

<p>
</p>
<p>
此網站為個人全端實作 MVC 5 網站，包含網站前台、管理者後台等兩區域<br>
前台使用者除可以進行網站瀏覽，亦可進行登入、註冊及發文、留言<br>
後臺管理者可以使用 CK Editor 進行訊息撰寫、亦可針對特定頁面資料進行 CRUD 存取
</p>
</div>
<hr/>


<h2 align="center" >功能介紹</h2>

> 身份分為「網站瀏覽者」、「前台會員」及「後台管理者」角色

### ► 網站瀏覽者

- 瀏覽網站頁面
  
- 前台會員註冊與登入
  
- 信件通知網站管理者


### ► 前台會員

- 具備「網站瀏覽者」之角色功能
  
- 啟用留言板功能，具備建立貼文、回覆文章功能


### ► 後台管理者 

- 編輯特定頁面之資訊

- CRUD特定頁面之資料

- 管理會員後台頁面存取權限


<h2 align="center">個人產出</h2>
 <p>

* 後端開發環境：
    * 框架：.NET Framework 4.7.2
    * 專案：ASP.NET MVC 5
      
* 開發技術：
  <div align="center">
    <img alt="Visual_Studio" src="https://img.shields.io/badge/Visual_Studio-5C2D91?style=for-the-badge&logo=visual%20studio&logoColor=white" />
    <img alt=".NET" src="https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" />
    <img alt="C#" src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white" />
    <img alt="MVC5" src="https://img.shields.io/badge/MVC_5-Green?style=for-the-badge">
    <img alt="SQL" src="https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white" />
    <img alt="Entity_Framework" src="https://img.shields.io/badge/Entity_Framework-yellow?style=for-the-badge">
    <img alt="LINQ" src="https://img.shields.io/badge/LINQ-8A2BE2?style=for-the-badge">
    <img alt="GIT" src="https://img.shields.io/badge/GIT-E44C30?style=for-the-badge&logo=git&logoColor=white" />
    <img alt="GitHUB" src="https://img.shields.io/badge/GitHub-100000?style=for-the-badge&logo=github&logoColor=white" />
  </div>
  
  - 區域路由：透過 Areas 建立後台網域，並將前台與後台會員權限分離

  - 資料庫存取：Microsoft SQL Server 搭配 Entity Framework Code First 以及 LINQ 進行資料庫存取
  
  - 權限控管：自定義 Filter 達成特定 Controller 的權限控管

  - 遞迴函式：將 Navbar 與 Sidebar 透過遞迴方式自動生成
  

* 個人 Git 規範：
  * Commit


    | 類型 | 格式 | 說明 |
    | :---: | :---: | :---: |
    | 新增功能 | Feat | 新增功能 |
    | 更新功能 | Update | 更新現有功能、程式碼格式調整 |


  * Branch


    | 類型 | 格式 |
    | :---: | :---: |
    | 開發功能 | dev |
    | 新增功能 | Feature-[branch name] |
    | 更新功能 | Update-[branch name] |


  * Git Flow

    <img src="https://raw.githubusercontent.com/David07994415/RocketFarmerProject/main/gitflowchar.png">

* 後端專案結構：
```
MVC5_SideProject
│  favicon.ico
│  Global.asax
│  Global.asax.cs
│  packages.config
│  Web.config
│  Web.Debug.config
│  Web.Release.config
│  WebApplication1.csproj
│  WebApplication1.csproj.user
│  
├─App_Start
│      BundleConfig.cs
│      FilterConfig.cs
│      RouteConfig.cs
│      
├─Areas
│  └─BackStage
│      │  BackStageAreaRegistration.cs
│      │  
│      ├─Controllers
│      │      AboutController.cs
│      │      AuthController.cs
│      │      Back_PremissionController.cs
│      │      CalendarController.cs
│      │      ConsultController.cs
│      │      HomeController.cs
│      │      IndexCoverController.cs
│      │      IndexLinkController.cs
│      │      IndexPurposeController.cs
│      │      JobController.cs
│      │      KnowledgeController.cs
│      │      LicenseController.cs
│      │      MasterController.cs
│      │      NewsController.cs
│      │      SurveyController.cs
│      │      
│      ├─Data
│      ├─Filter
│      │      AddBackLayoutComponent.cs
│      │      CheckPremission.cs
│      │      UpdateMemberPremission.cs
│      │      
│      └─Views
│          │  web.config
│          │  _ViewStart.cshtml
│          │  
│          ├─About
│          │      Index.cshtml
│          │      
│          ├─Auth
│          │      Create.cshtml
│          │      Delete.cshtml
│          │      Details.cshtml
│          │      Edit.cshtml
│          │      Index.cshtml
│          │      
│          ├─Back_Premission
│          │      Edit.cshtml
│          │      Index.cshtml
│          │      
│          ├─Calendar
│          │      Index.cshtml
│          │      
│          ├─Consult
│          │      Index.cshtml
│          │      
│          ├─Default
│          ├─Home
│          │      Index.cshtml
│          │      Login.cshtml
│          │      Register.cshtml
│          │      
│          ├─IndexCover
│          │      Create.cshtml
│          │      Delete.cshtml
│          │      Edit.cshtml
│          │      Index.cshtml
│          │      
│          ├─IndexLink
│          │      Create.cshtml
│          │      Delete.cshtml
│          │      Edit.cshtml
│          │      Index.cshtml
│          │      
│          ├─IndexPurpose
│          │      Index.cshtml
│          │      
│          ├─Job
│          │      Index.cshtml
│          │      
│          ├─Knowledge
│          │      Create.cshtml
│          │      Delete.cshtml
│          │      Details.cshtml
│          │      Edit.cshtml
│          │      Index.cshtml
│          │      
│          ├─License
│          │      Index.cshtml
│          │      
│          ├─Master
│          │      Create.cshtml
│          │      Delete.cshtml
│          │      Details.cshtml
│          │      Edit.cshtml
│          │      Index.cshtml
│          │      
│          ├─News
│          │      Create.cshtml
│          │      Delete.cshtml
│          │      Details.cshtml
│          │      Edit.cshtml
│          │      Index.cshtml
│          │      
│          ├─Shared
│          │      _Layout.cshtml
│          │      _LayoutBackMainPage.cshtml
│          │      _PartialMenuView.cshtml
│          │      
│          └─Survey
│                  Index.cshtml
│                  
├─bin
│        ...
│          
├─Ck5
│      ckeditor.js
│      ckeditor.js.map
│      
├─Content
│        ...
│          
├─Controllers
│      AboutController.cs
│      CalendarController.cs
│      ConsultController.cs
│      ContactController.cs
│      HomeController.cs
│      JobController.cs
│      KnowledgeController.cs
│      LicenseController.cs
│      MasterController.cs
│      Member_BulletinController.cs
│      Member_LoginController.cs
│      Member_LogoutController.cs
│      Member_RegisterController.cs
│      NewsController.cs
│      SurveyController.cs
│      
├─Filter
│      AddLayoutComponent.cs
│      MemberAuthFilter.cs
│      
├─Migrations
│      ...
│      Configuration.cs
│      
├─Models
│  │  Bulletin.cs
│  │  Business.cs
│  │  BusinessCategory.cs
│  │  DbModel.cs
│  │  Directory.cs
│  │  IndexCover.cs
│  │  IndexLink.cs
│  │  IndexPurpose.cs
│  │  Knowledge.cs
│  │  Master.cs
│  │  Member.cs
│  │  Message.cs
│  │  ModelBase.cs
│  │  News.cs
│  │  Permission.cs
│  │  WebContent.cs
│  │      
│  └─ViewModels
│          ContactFrontViewModel.cs
│          CreateBackBusinessViewModel.cs
│          CreateBackIndexCoverViewModel.cs
│          CreateBackIndexLinkViewModel.cs
│          CreateBackKnowledgeViewModel.cs
│          CreateBackMasterViewModel.cs
│          CreateBackNewsViewModel.cs
│          CreateFrontBulletinMainViewModel.cs
│          CreateMemberShipFrontViewModel.cs
│          DirectoryBackViewModel.cs
│          DirectoryFrontViewModel.cs
│          EditBackAboutViewModel.cs
│          EditBackIndexPurposeViewModel.cs
│          HomeDataViewModel.cs
│          LoginBackViewModel.cs
│          RegisterBackViewModel.cs
│          
├─obj
│       ...
│      
├─Scripts
│        ...
│          
├─Utility
│      Encrypt.cs
│      Mailing.cs
│      
└─Views
    │  Web.config
    │  _ViewStart.cshtml
    │  
    ├─About
    │      Index.cshtml
    │      
    ├─Calendar
    │      Index.cshtml
    │      
    ├─Consult
    │      Index.cshtml
    │      
    ├─Contact
    │      Index.cshtml
    │      
    ├─Home
    │      Index.cshtml
    │      
    ├─Job
    │      Index.cshtml
    │      
    ├─Knowledge
    │      Index.cshtml
    │      
    ├─License
    │      Index.cshtml
    │      
    ├─Master
    │      Index.cshtml
    │      MasterDetail.cshtml
    │      
    ├─Member_Bulletin
    │      Create.cshtml
    │      Detail.cshtml
    │      Index.cshtml
    │      Reply.cshtml
    │      
    ├─Member_Login
    │      Index.cshtml
    │      
    ├─Member_Logout
    │      Index.cshtml
    │      
    ├─Member_Register
    │      Index.cshtml
    │      
    ├─News
    │      Index.cshtml
    │      NewsDetail.cshtml
    │      
    ├─Shared
    │      Error.cshtml
    │      _Layout.cshtml
    │      _LayoutFire.cshtml
    │      _LayoutHomePage.cshtml
    │      _LayoutPage.cshtml
    │      _PartialBanner.cshtml
    │      _PartialSideBar.cshtml
    │      
    └─Survey
            Index.cshtml
```

</p>
<hr/>


