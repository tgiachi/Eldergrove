# Change Log

All notable changes to this project will be documented in this file. See [versionize](https://github.com/versionize/versionize) for commit guidelines.

<a name="0.1.0-alpha.1"></a>
## [0.1.0-alpha.1](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.1.0-alpha.1) (2024-10-09)

### Features

* **ColorsModule:** change AddColor method to accept variable arguments for color values to enhance flexibility ([a00dd50](https://www.github.com/tgiachi/Eldergrove/commit/a00dd50ef9bc31d151192813694f7f3ef4a827f9))
* **door-component:** implement DoorComponent to manage door states in the game ([bd7901a](https://www.github.com/tgiachi/Eldergrove/commit/bd7901a0932d5c26ae0452ba2183bcd78e40d222))
* **ScriptEngineService:** add AddModulesDirectory method to configure Lua module search paths for better script management ([7a0d8ac](https://www.github.com/tgiachi/Eldergrove/commit/7a0d8acabd7b74e96ac8b655c0dcad1a7fd9c5f1))
* **Taskfile:** add bump task to automate versioning and pushing tags to main branch ([050ac03](https://www.github.com/tgiachi/Eldergrove/commit/050ac0362e15d3f3f1fb3c78c70578076d1e84a3))
* **TileServiceModule:** change AddTile method to accept LuaTable for better integration with Lua scripts ([4473729](https://www.github.com/tgiachi/Eldergrove/commit/4473729ab0feaafd3fb76de2ed7d4569a17e5472))

### Bug Fixes

* **ScriptEngineService:** catch specific LuaException instead of general Exception to improve error handling ([2ee13d7](https://www.github.com/tgiachi/Eldergrove/commit/2ee13d7d1dfaf80f379fc7989d4a96f97aadfff3))
* **ScriptEngineService:** enhance error logging to include formatted exception details for better debugging ([51ed8ef](https://www.github.com/tgiachi/Eldergrove/commit/51ed8efa7f6186c25ae817b91afff55c07438bc0))

<a name="0.1.0-alpha.0"></a>
## [0.1.0-alpha.0](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.1.0-alpha.0) (2024-10-08)

### Features

* replace Jint with NLua for scripting support in Eldergrove engine to enhance performance and flexibility ([a7a7bfe](https://www.github.com/tgiachi/Eldergrove/commit/a7a7bfe71eeee9c956cc5e6dd805d7fd31dbd8e8))

<a name="0.0.1"></a>
## [0.0.1](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.0.1) (2024-10-08)

### Features

* add NamesObject class for JSON data representation and data loader integration ([bde254f](https://www.github.com/tgiachi/Eldergrove/commit/bde254f999be418e92b37ef1928149f6e4d93214))
* initialize Eldergrove project with .editorconfig, .gitignore, and solution files ([7ee4c74](https://www.github.com/tgiachi/Eldergrove/commit/7ee4c741f9d3ba3b15884f526e46e43e30f2f387))
* **attributes:** add DataLoaderTypeAttribute to define data loader types for classes ([2c556fb](https://www.github.com/tgiachi/Eldergrove/commit/2c556fbc81c51f589d0102d753beec727b965a97))
* **AutostartService:** implement AutostartServiceAttribute to manage service startup order ([af871ca](https://www.github.com/tgiachi/Eldergrove/commit/af871ca40ff4abd524eb918b4ad24a4b12c8987b))
* **colors:** add ColorObject and PropObject classes for color and prop data representation ([c845872](https://www.github.com/tgiachi/Eldergrove/commit/c845872a29b056a8d5b2a4ec3dec09a5fd079fe1))
* **ColorsModule:** add ColorsModule to manage color-related functionalities and register it in EldergroveEngine to enhance script capabilities ([c5e2007](https://www.github.com/tgiachi/Eldergrove/commit/c5e2007d226313b25731c0b2cba7e5cb8fc93398))
* **ColorsModule:** change AddColor method to accept a single int array for color values for better clarity ([5b6d267](https://www.github.com/tgiachi/Eldergrove/commit/5b6d267d3e064c62625dde968aa703460bca49b9))
* **core:** add IMessageBusService interface and MessageBusService implementation for messaging functionality ([91a9e39](https://www.github.com/tgiachi/Eldergrove/commit/91a9e392e6e6ad2ad95178830413b10590dfa0d7))
* **core:** add versioning support with IVersionService and VersionService to retrieve application version ([0014eab](https://www.github.com/tgiachi/Eldergrove/commit/0014eab90f5c80a4e7a0a5b6085572f6bdb48c6d))
* **core:** update Microsoft.Extensions packages to latest versions for improved stability and features ([34578d9](https://www.github.com/tgiachi/Eldergrove/commit/34578d9af87aa3a5c25285f720ec8bef19aa7628))
* **CSharpJsConverterUtils:** add additional type mappings for C# to JavaScript conversion to enhance compatibility with C# types ([9f56085](https://www.github.com/tgiachi/Eldergrove/commit/9f56085813621f8bfc0c6aaf4b79f18a2146c6c6))
* **data:** add TileSetObject class to represent tile set data structure ([c65a410](https://www.github.com/tgiachi/Eldergrove/commit/c65a4104bc84b149c72497cd5844269cb62ce67f))
* **DataLoaderService:** add SubscribeData method to allow subscribers to receive updates on data changes ([ba5063d](https://www.github.com/tgiachi/Eldergrove/commit/ba5063d78a92534b3d415b126d6ddff691cf860e))
* **DirectoryConfig:** add DirectoryConfig class to manage root directory and provide directory path generation ([3563cc7](https://www.github.com/tgiachi/Eldergrove/commit/3563cc730c3b0423eced0dc1fa8e99d26c835ea1))
* **DirectoryConfig:** add indexer to access directories by DirectoryType for easier access ([64d7bae](https://www.github.com/tgiachi/Eldergrove/commit/64d7baef1f7a18530972618395c27ff1ce4a6f01))
* **DirectoryConfig:** remove unnecessary blank lines for cleaner code ([1c09ff9](https://www.github.com/tgiachi/Eldergrove/commit/1c09ff9ba633a6cf5d51ef109556ef67c099b4ae))
* **EldergroveEngine:** register TileServiceModule to enhance module functionality ([c0d0775](https://www.github.com/tgiachi/Eldergrove/commit/c0d0775a8eda9cff99a2d32e634b4cf4b1a101db))
* **EventDispatcherService:** implement event dispatching, subscription, and unsubscription methods to enhance event handling capabilities ([fa9b99b](https://www.github.com/tgiachi/Eldergrove/commit/fa9b99bf7c502ab3c7299a9bb353cef65d4ef011))
* **EventsModule:** add EventsModule to handle event dispatching and subscription ([1cef762](https://www.github.com/tgiachi/Eldergrove/commit/1cef762a09438e303ec8f6a66f11711cdd8c265a))
* **IEldergroveEngine:** remove public access modifier from interface methods for consistency ([ffd3431](https://www.github.com/tgiachi/Eldergrove/commit/ffd34312ab1028f779fb841f475868fcb37bcee1))
* **IMessageBusService:** extend interface to inherit from IEldergroveService for better service management ([f620a96](https://www.github.com/tgiachi/Eldergrove/commit/f620a964c6ba296e4c39998273dffecfd53dda0d))
* **ItemGameObject:** implement ToString method for better item representation ([07f45f3](https://www.github.com/tgiachi/Eldergrove/commit/07f45f32ea063f6677fa7b57e15f67831dca3539))
* **ItemObject:** add ItemObject class to represent items in the game with properties for ID, symbol, colors, and category ([a247066](https://www.github.com/tgiachi/Eldergrove/commit/a247066829f3b3e001ee3f3c9704d8da97b18c46))
* **Json:** add JsonStateObject and JsonSymbolDataObject classes to represent state and symbol data in JSON format ([c3c150b](https://www.github.com/tgiachi/Eldergrove/commit/c3c150b2d5d969c8bc822b9592f11466e09d297f))
* **Keybinding:** add KeybindingObject and ActionCommandService for command management ([beaf383](https://www.github.com/tgiachi/Eldergrove/commit/beaf383c427eb8fa996260d31d3019612be96131))
* **LoggerPanel:** implement logging functionality in LoggerPanel to display log messages with timestamps and colors based on log levels ([bc9531b](https://www.github.com/tgiachi/Eldergrove/commit/bc9531b5514f81fb953a76ff6cfac2f20224bc02))
* **logging:** implement event bus logging with Serilog for better event handling ([8b1758a](https://www.github.com/tgiachi/Eldergrove/commit/8b1758a0686ecc1a09cca5bbe14851865796ae09))
* **MapLayerType:** add MapLayerType enum to define different map layers for better organization and clarity in the codebase ([c6c6431](https://www.github.com/tgiachi/Eldergrove/commit/c6c643110806d108f15863cd9a57720ef992d7ba))
* **NameGenerator:** implement INameGeneratorService and its functionality to manage names ([4c2ce9f](https://www.github.com/tgiachi/Eldergrove/commit/4c2ce9fabcfcc48613dae1ffc556b6d556f9814e))
* **NameGenerator:** update logging levels for better traceability and add new name files for fantasy characters ([bf1699c](https://www.github.com/tgiachi/Eldergrove/commit/bf1699c5f409d5e5b9fcef744f5faa3f66609aab))
* **Program.cs:** add custom font configuration for the game to enhance visual appearance ([07cf575](https://www.github.com/tgiachi/Eldergrove/commit/07cf57547fafe411294288e2b0f8708536b162ef))
* **PropGameObject:** add CanDestroy property to indicate if the prop can be destroyed ([7a2fbd4](https://www.github.com/tgiachi/Eldergrove/commit/7a2fbd49d011e0ed1c5bfb24289ac25e65b0a18f))
* **PropGameObject:** add PropGameObject class to represent interactive props in the game ([774538e](https://www.github.com/tgiachi/Eldergrove/commit/774538e6ef1223a1f8a9580559f4de61f3a67f2c))
* **PropGameObject:** change base class to RogueLikeCell and update constructor to accept ColoredGlyph for improved rendering ([2773bb6](https://www.github.com/tgiachi/Eldergrove/commit/2773bb6edb60e506f26837fdeb9602fed4a35f53))
* **PropObject:** add Container property to support item storage in props ([9092b4d](https://www.github.com/tgiachi/Eldergrove/commit/9092b4d78bd52e483a2beef17d5a7e22dbc3e19d))
* **PropObject:** add OnDestroy property to support random destruction behavior ([5a2f132](https://www.github.com/tgiachi/Eldergrove/commit/5a2f13238415f552d871605648f437f4e6e01211))
* **PropService:** add IPropService interface and PropService implementation to manage props ([1e5bd35](https://www.github.com/tgiachi/Eldergrove/commit/1e5bd350c1cc6d572140650f27cf35d3cffb9311))
* **RandomExtension:** add RandomExtension class with methods to get random elements from an IEnumerable to enhance collection manipulation ([9417c6e](https://www.github.com/tgiachi/Eldergrove/commit/9417c6e95cb4a284d350b421610cc080d739312f))
* **ScriptEngine:** implement script engine service with attributes for script functions and modules ([e760920](https://www.github.com/tgiachi/Eldergrove/commit/e76092073701ca580e6b41dfcee8e8c9148dd7d8))
* **ScriptEngineService:** add AddContextVariable method to manage context variables dynamically ([43f3c28](https://www.github.com/tgiachi/Eldergrove/commit/43f3c2896d96e9b05ee6286f0a6df2fd366ed083))
* **solution:** add Eldergrove.Gui project to the solution for GUI development ([303e200](https://www.github.com/tgiachi/Eldergrove/commit/303e2007fc6743953725bd836649fcec12b9f48e))
* **solution:** add Eldergrove.Tests project to the solution for unit testing ([caa4d81](https://www.github.com/tgiachi/Eldergrove/commit/caa4d815708481e065a58b422338b2db406f6758))
* **solution:** add Eldergrove.Ui.Core project to the solution for UI components ([d8c29fc](https://www.github.com/tgiachi/Eldergrove/commit/d8c29fc81fb6eb111bbabc0e09bb0afbb98a1196))
* **Taskfile:** add clean and restore tasks for better build management ([c07808b](https://www.github.com/tgiachi/Eldergrove/commit/c07808b2e7e7fcd193e0aa4b17517a08ae8290f1))
* **Taskfile:** add Taskfile.yml to define build, test, and deploy tasks for improved project automation ([aa8e19f](https://www.github.com/tgiachi/Eldergrove/commit/aa8e19f58f9bdbf492016ec1c050a65dfaba06f9))
* **Taskfile:** update test command to run dotnet test for better testing process ([ac42797](https://www.github.com/tgiachi/Eldergrove/commit/ac427976910d4753a8d56e71b2e7013d33cbf070))
* **TileEntry:** add Symbol property to represent sprite or animation identifiers ([77d9594](https://www.github.com/tgiachi/Eldergrove/commit/77d95943938bc76236937a4ce40e9b2b3cc01b28))
* **TileEntry:** add TileEntry class to represent tile data with tags ([c154a4f](https://www.github.com/tgiachi/Eldergrove/commit/c154a4fbb2eef231d3a3e8a644312bd2c60597c3))
* **TileService:** add GetTile method to retrieve a tile by its ID for improved functionality and usability ([5768c28](https://www.github.com/tgiachi/Eldergrove/commit/5768c2865d13d03d8e1053dea3e4145dfdadd595))
* **TileService:** implement ITileService to manage tile entries and add tile functionality ([004386d](https://www.github.com/tgiachi/Eldergrove/commit/004386d5066bd0c59e9c4ea4a6b122084f05805a))
* **TileSet:** create TileSetObject class and IJsonDataObject interface to represent tile set data structure ([ce9a3af](https://www.github.com/tgiachi/Eldergrove/commit/ce9a3afbb6840388f7d3cfbd82605f5521405ae0))
* **TileSetObject:** add DataLoaderType attribute to TileSetObject for better data loading integration ([c4daa20](https://www.github.com/tgiachi/Eldergrove/commit/c4daa2078c2f2583cdd68431a3965620f0d9fe04))

### Bug Fixes

* **MessageBusService:** add null checks for message and action parameters to prevent runtime exceptions ([2bbe3e1](https://www.github.com/tgiachi/Eldergrove/commit/2bbe3e1afe569a7eee6486283a0a9e3f0acc5e98))
* **ScriptEngineService.cs:** remove parameter from DebugMode method to align with updated library API ([76a9e8a](https://www.github.com/tgiachi/Eldergrove/commit/76a9e8a0adff5b5adc15bb447d88f22fe08d9a03))
