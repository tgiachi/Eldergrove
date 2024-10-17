# Change Log

All notable changes to this project will be documented in this file.

<a name="0.3.0-alpha.0"></a>
## [0.3.0-alpha.0](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.3.0-alpha.0) (2024-10-17)

### Features

* **components:** add DestroyComponent and PropHealthComponent to manage destruction and health of props ([6fd5bb0](https://www.github.com/tgiachi/Eldergrove/commit/6fd5bb011691ad06617ad9ef489762ee4e460454))
* **InventoryComponent:** add InventoryComponent to manage items for game objects ([f9508bd](https://www.github.com/tgiachi/Eldergrove/commit/f9508bdb45acf8aa1e39e2cee6595bc98e766f21))
* **JsonPortalObject:** add JsonPortalObject class to represent portal data in JSON format ([6fba208](https://www.github.com/tgiachi/Eldergrove/commit/6fba20803114ee25bcc41ff89d3882e4f561d1ef))

<a name="0.2.0"></a>
## [0.2.0](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.2.0) (2024-10-17)

### Features

* **DoorComponent.cs:** add message logging for door open/close events to enhance user feedback ([c2de00a](https://www.github.com/tgiachi/Eldergrove/commit/c2de00ae8a08d08866c6d3ea1db19aafdb31b4aa))
* **logging:** implement message logging system with MessageLogEvent and MessageLogData ([d63202f](https://www.github.com/tgiachi/Eldergrove/commit/d63202fbc5223479382e4d287f3678f2515caaad))
* **MapExtension:** add PreAllocatePoints method to generate a list of points based on width, height, and starting point for better area management ([dba3bd9](https://www.github.com/tgiachi/Eldergrove/commit/dba3bd942ddf3058bc4cbcc74da1d3ac8450b188))
* **MapGenService:** implement town generation logic in MapGenService to support new map type ([c1e619d](https://www.github.com/tgiachi/Eldergrove/commit/c1e619d53678771d43fd597f0f873318a2109410))

### Bug Fixes

* **MapFabricObject:** update Width and Height properties to use ToArray for accurate dimensions ([ad312de](https://www.github.com/tgiachi/Eldergrove/commit/ad312debd771903fc3e25d2695ef28d8277aea24))

<a name="0.2.0-alpha.5"></a>
## [0.2.0-alpha.5](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.2.0-alpha.5) (2024-10-16)

### Features

* **dialogs:** implement dialog system with nested options for NPC interactions ([e1c57fb](https://www.github.com/tgiachi/Eldergrove/commit/e1c57fb35fe1f9ef10b5b3ecbec377a7ba6c34e7))
* **EntityAttackAction.cs:** add new class EntityAttackAction to handle NPC attacks ([a1c1086](https://www.github.com/tgiachi/Eldergrove/commit/a1c108620ca0cdd9828d1c4c4d9d2ff0343fc09f))
* **GameConfig:** add SchedulerConfig to manage game scheduling settings ([a2194d3](https://www.github.com/tgiachi/Eldergrove/commit/a2194d3f57bd5c7f486a9dc5a411ba8456b467cb))
* **JsonSkillsObject:** add Gold property to JsonSkillsObject for better resource management ([e238ee1](https://www.github.com/tgiachi/Eldergrove/commit/e238ee1418e732332d24654ba12c032e2d684636))
* **npc-actions:** implement entity movement, perform actions, and search for entities to enhance NPC behavior ([4b404a1](https://www.github.com/tgiachi/Eldergrove/commit/4b404a14885eaa2b9bff1225764d7f19d876af76))
* **NpcDieComponent:** add NpcDieComponent to handle NPC death and blood effects ([755f271](https://www.github.com/tgiachi/Eldergrove/commit/755f271ff19b65c3d7d895ae791f675a2aa1a10c))
* **NpcDieComponent:** add random blood position generation within a radius to enhance visual effects on NPC death ([130a16b](https://www.github.com/tgiachi/Eldergrove/commit/130a16b1246567a10f1a4e7388c4db7d3cd82602))
* **PlayerFOVController:** refactor OnAdded method to use block syntax for clarity ([1ffa117](https://www.github.com/tgiachi/Eldergrove/commit/1ffa1171345011306ac57d262a0b833664be7ec4))
* **props.json:** add door properties to window object to define door states and their attributes ([4657f01](https://www.github.com/tgiachi/Eldergrove/commit/4657f0157edba16240e1de067b68117a7565d97b))
* **SkillsComponent:** add new attributes for character skills and alignment type to enhance gameplay mechanics ([a59aaf1](https://www.github.com/tgiachi/Eldergrove/commit/a59aaf148d048e85c1f6fcd57f45bf05cf339823))
* **TileAnimationComponent:** enhance tile animation with color transition support for foreground and background colors ([5957b86](https://www.github.com/tgiachi/Eldergrove/commit/5957b866450a4c44123a6f2eb378331cd817e628))
* **TileService:** add handling for tile symbols starting with "!!" and "##" to enhance tile processing logic ([c37e503](https://www.github.com/tgiachi/Eldergrove/commit/c37e50397a61845982391f5decfdc830662a740f))
* **TimedRemoveComponent:** add TimedRemoveComponent to handle timed entity removal in the game ([159d27e](https://www.github.com/tgiachi/Eldergrove/commit/159d27e4a8577fc37bfadb39405269b89bc45be1))

<a name="0.2.0-alpha.4"></a>
## [0.2.0-alpha.4](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.2.0-alpha.4) (2024-10-14)

### Features

* add new Bar and Text service classes and definitions to support future functionality ([4bbff52](https://www.github.com/tgiachi/Eldergrove/commit/4bbff522c5e4ea2651a6315b0553e3f14f738ceb))
* **data-structure:** implement BarDefinition and BarObject classes to define bar data structures ([2cd239d](https://www.github.com/tgiachi/Eldergrove/commit/2cd239d86c8b48539080f7b84f3391f992522eeb))
* **EntityMovement:** add entity movement action and handling for door interactions to enhance gameplay mechanics ([f7fb9b3](https://www.github.com/tgiachi/Eldergrove/commit/f7fb9b3c3d7648ba521847c293c6eebf058a0cb2))
* **solution:** add Visual Studio solution file to manage project structure ([f6fef8e](https://www.github.com/tgiachi/Eldergrove/commit/f6fef8e72f9c5da6af961afecb6ceeeb3a00988a))

<a name="0.2.0-alpha.3"></a>
## [0.2.0-alpha.3](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.2.0-alpha.3) (2024-10-13)

### Features

* **AiContext:** add IsPlayerInRange method to check if player is within a specified radius for improved gameplay mechanics ([70327d9](https://www.github.com/tgiachi/Eldergrove/commit/70327d967f704d248013f66a23996ac4a48b0373))
* **AiContext:** add layer parameter to GetEntitiesAtRange method for improved entity retrieval ([3b3a06e](https://www.github.com/tgiachi/Eldergrove/commit/3b3a06ecf734c0f661c8b55f70482b82a9009028))
* **AiContext:** fix random direction generation logic to ensure valid range ([03390fa](https://www.github.com/tgiachi/Eldergrove/commit/03390fae0979d78c53ab6856ae6be9250410ba43))
* **dialogs:** add DialogNode, DialogObject, and DialogOption classes to support dialog structure ([e21849a](https://www.github.com/tgiachi/Eldergrove/commit/e21849aba5acd6f829439ef96a0cfdcc76fd800b))
* **IMapGenService:** add a new method to handle map generation and management ([97f4e6b](https://www.github.com/tgiachi/Eldergrove/commit/97f4e6be0864ac75a5010a2ce4951a8c5489f9a9))
* **PlayerFOVController:** change FOVRadius from init-only to settable to allow dynamic updates ([b67737d](https://www.github.com/tgiachi/Eldergrove/commit/b67737d2372d8f8c35bd0cba2f9212b4ea9ab08f))
* **PropObject:** change Name property to nullable string to allow for missing names ([fb8472f](https://www.github.com/tgiachi/Eldergrove/commit/fb8472fa3826dc3abc2a32c8396e0e145cc23a37))
* **TileAnimation:** implement TileAnimationComponent for animated tiles ([f2ed6ca](https://www.github.com/tgiachi/Eldergrove/commit/f2ed6cae5f2770d922e4f73623aa545f14cb5151))

<a name="0.2.0-alpha.2"></a>
## [0.2.0-alpha.2](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.2.0-alpha.2) (2024-10-11)

### Features

* **ActionResult:** add AlternateAction property to ActionResult for enhanced action handling ([25161e4](https://www.github.com/tgiachi/Eldergrove/commit/25161e4f5ae1685fd3045872eacb3b44a1431046))
* **AiComponent:** add entity reference to context for improved AI processing ([9cb5f55](https://www.github.com/tgiachi/Eldergrove/commit/9cb5f55896936c56e71fe24bdfe08c4d673fe14f))
* **AiComponent:** change TakeTurn method to return IEnumerable<ISchedulerAction> for better action scheduling ([45943f7](https://www.github.com/tgiachi/Eldergrove/commit/45943f72dbf927b35f67f66415fc4e6d67f688f0))
* **AiComponent:** refactor AI components to use new ActionContext for command registration and execution ([151195c](https://www.github.com/tgiachi/Eldergrove/commit/151195cc01e29a5406eb1a02e2cf00c68dc9b577))
* **AiContext:** add EmptyActionList method to return an empty action list for AI context ([b0894ef](https://www.github.com/tgiachi/Eldergrove/commit/b0894ef7c587b2dc56aa3cac79cd18ae619cb9a6))
* **AiContext:** add ToString method for better debugging and logging of AiContext ([21e7294](https://www.github.com/tgiachi/Eldergrove/commit/21e72943a2352e2b589988d960434b2abf755bca))
* **AiContext.cs:** add state management methods to AiContext for better entity state handling ([ffe0294](https://www.github.com/tgiachi/Eldergrove/commit/ffe02941897533b06dc5d78134764f2aa59b6875))
* **AiContext.cs:** implement indexer for AiContext to simplify state access and modification ([5f246f4](https://www.github.com/tgiachi/Eldergrove/commit/5f246f4b44ef192fb79fce7ca8f8e94cf8f03c11))
* **EntityMovement:** implement EntityMovementAction to handle NPC movement logic ([ff80cdc](https://www.github.com/tgiachi/Eldergrove/commit/ff80cdca78a57164a1757dc96cd77e555f1f4195))
* **Keybinding:** implement keybinding action system to enhance user input handling ([2996d3d](https://www.github.com/tgiachi/Eldergrove/commit/2996d3d832111b6e9ef4d93abd62c6c17e33d760))
* **KeybindingActions:** implement base class for player movement actions and create specific action for moving up ([82d47d0](https://www.github.com/tgiachi/Eldergrove/commit/82d47d07d7de328fc659d7c4befd27bf5ff2013d))
* **keybindings:** implement context-based keybinding system for player movement actions ([15d7c8d](https://www.github.com/tgiachi/Eldergrove/commit/15d7c8dd258cde9a3f28838c92ebff494970bcf3))
* **MapGenService:** add event handler for object removal to manage actionable entities in the map ([1d9969e](https://www.github.com/tgiachi/Eldergrove/commit/1d9969e6a1972d5f4006a9085af49f0cd7035304))
* **PlayerMovement:** remove gameMap dependency from PlayerMovementAction to simplify the constructor and use player’s current map directly ([7e32b7d](https://www.github.com/tgiachi/Eldergrove/commit/7e32b7d2771f85e9b39b2236f119ee4a0cd561f9))
* **SchedulerService:** add methods to manage actionable entities in the scheduler ([a857de2](https://www.github.com/tgiachi/Eldergrove/commit/a857de2ec22f10d3678b2cef04e7fb3881890596))
* **SchedulerService:** add stopwatch to measure execution time of TickAsync method for performance monitoring ([3f4c1ee](https://www.github.com/tgiachi/Eldergrove/commit/3f4c1eef960d66ea691922ac5c36e5053fcac81f))

### Bug Fixes

* **GameMap.cs:** change distance calculation method from Manhattan to Euclidean to improve pathfinding accuracy ([74f0c4b](https://www.github.com/tgiachi/Eldergrove/commit/74f0c4b8a5b06c6bb571ae735afff61f9a0722e1))
* **MapFabricObject.cs:** set default value of CanRotate to false to prevent unintended rotations ([8b64c8d](https://www.github.com/tgiachi/Eldergrove/commit/8b64c8d86a5996841e092805255090453ad289d3))

<a name="0.2.0-alpha.1"></a>
## [0.2.0-alpha.1](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.2.0-alpha.1) (2024-10-10)

### Features

* add PlayerFOVController to manage player's field of view and visibility ([fb6a129](https://www.github.com/tgiachi/Eldergrove/commit/fb6a12931d3fcee9fc38f1dcd230e2c9b3ad9e9a))
* **default_fabrics.json:** add a cat NPC to the default fabrics for enhanced gameplay ([15a51da](https://www.github.com/tgiachi/Eldergrove/commit/15a51da88891685c635d4fcbc1ada4da222317e1))
* **DoorComponent:** comment out appearance copying logic to prevent errors when door state changes ([37cc582](https://www.github.com/tgiachi/Eldergrove/commit/37cc582752cab8a760a45100aacdf6012e26971d))
* **extensions:** add MapExtension class with FindFreeArea method to locate free areas on the map ([30509db](https://www.github.com/tgiachi/Eldergrove/commit/30509db253e2df54bdf3f9b00e5f07066fe18976))
* **fonts:** add C64 font configuration and corresponding image file to enhance GUI aesthetics ([dfe82cd](https://www.github.com/tgiachi/Eldergrove/commit/dfe82cdecac8d70cf114fef9288cfc46d34df3a5))
* **IMapGenService:** change GenerateMapAsync method to return GameMap to provide generated map data ([b930668](https://www.github.com/tgiachi/Eldergrove/commit/b93066838a194db211d5e7d04a4ab2ae32e16a4e))
* **MapGenService:** refactor GenerateFabric method to use tuples for wall and floor parameters for better clarity and structure ([f997a5a](https://www.github.com/tgiachi/Eldergrove/commit/f997a5a2021a3f532dfebfe7ecbd11461100ac6b))
* **SkillsComponent:** add Gold property to SkillsComponent for tracking player gold ([4109452](https://www.github.com/tgiachi/Eldergrove/commit/4109452228b4b30cfdb94cf26b30ffaf58f10491))

### Bug Fixes

* **FindFreeArea:** increase maxAttempts from 1000 to 200 to improve area search success rate ([094506e](https://www.github.com/tgiachi/Eldergrove/commit/094506eea11a835122a0117ae66a140772198c3d))

<a name="0.2.0-alpha.0"></a>
## [0.2.0-alpha.0](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.2.0-alpha.0) (2024-10-10)

### Features

* add initial project structure and core components for Eldergrove engine ([75d20dd](https://www.github.com/tgiachi/Eldergrove/commit/75d20dd336847dca06b3fcb90f6b304f1b21e4f3))
* **actions:** add PlayerMovementAction class to handle player movement logic ([6603bc4](https://www.github.com/tgiachi/Eldergrove/commit/6603bc4f0d6291adc1c5b5e610cb8430e151f745))
* **actions:** implement TimedAction and PlayerMovementAction for better action scheduling and player movement handling ([bffa824](https://www.github.com/tgiachi/Eldergrove/commit/bffa82477ddb61f4a1bac748f8a15b05680879dd))
* **events:** add EngineReadyEvent and MapGeneratedEvent to handle engine state changes ([150a4a9](https://www.github.com/tgiachi/Eldergrove/commit/150a4a90cae7fdd8b27e8b2d360de04c77a0919a))
* **MainScreen:** add MainScreen class to manage the main console interface ([1f6a74d](https://www.github.com/tgiachi/Eldergrove/commit/1f6a74df0c37231f39d386792e142f2cfd57fe59))
* **MapGenerator:** add Wall and Floor properties to MapGeneratorObject for better map customization ([ee5f433](https://www.github.com/tgiachi/Eldergrove/commit/ee5f433d727d63a4f229d4c761a7faa096ead1a5))
* **scheduler:** implement abstract scheduler action and dummy action for task scheduling ([9e9eaaf](https://www.github.com/tgiachi/Eldergrove/commit/9e9eaaffc45927477a22e7dfecbd099eeae7e637))

<a name="0.1.1"></a>
## [0.1.1](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.1.1) (2024-10-09)

<a name="0.1.0"></a>
## [0.1.0](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.1.0) (2024-10-09)

### Features

* **SchedulerService:** add ISchedulerService interface and its implementation ([233a20e](https://www.github.com/tgiachi/Eldergrove/commit/233a20e3b388e9675b091e9faa6c5dd6c27ada77))

<a name="0.1.0-alpha.5"></a>
## [0.1.0-alpha.5](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.1.0-alpha.5) (2024-10-09)

### Features

* **GameConfig:** add GameConfig class to define map dimensions for the game ([05adaf7](https://www.github.com/tgiachi/Eldergrove/commit/05adaf7634a18c43edd1acd5ce608ab94db847a3))

<a name="0.1.0-alpha.4"></a>
## [0.1.0-alpha.4](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.1.0-alpha.4) (2024-10-09)

### Features

* **Ai:** implement AiContext and GameMap classes to manage AI state and map representation ([558e0ed](https://www.github.com/tgiachi/Eldergrove/commit/558e0ed82044166dd2c9abd823f8ea7a04e191f1))
* **AiComponent:** implement AI component for NPCs to manage behavior and actions ([f02a03d](https://www.github.com/tgiachi/Eldergrove/commit/f02a03dbde8e36e5aaec2aa2a5b3630f633325bf))
* **MapFabricObject:** extend MapFabricObject to include Name and Category properties for better categorization ([301f658](https://www.github.com/tgiachi/Eldergrove/commit/301f658f587f51f73e23208c8ef1c6515deaeb44))
* **readme:** update dynamic scripting engine from Jint to NLua for improved functionality ([ebefc40](https://www.github.com/tgiachi/Eldergrove/commit/ebefc401aeb4db85915d93ce04093bcf4efec632))

<a name="0.1.0-alpha.3"></a>
## [0.1.0-alpha.3](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.1.0-alpha.3) (2024-10-09)

### Features

* **NpcModule:** add NpcModule to register NPC-related functionalities and build NPC game objects ([c1ecc92](https://www.github.com/tgiachi/Eldergrove/commit/c1ecc92de7a2611f4eb7d734c278502e515bb9a4))
* **ScriptModule:** rename script function to "add_ctx" for clarity and add "on_start" function to register bootstrap ([4a5065c](https://www.github.com/tgiachi/Eldergrove/commit/4a5065c8217486c7e08328357efa661ce17f3b92))
* **SkillsComponent:** add SkillsComponent to manage NPC skills and health ([d86e050](https://www.github.com/tgiachi/Eldergrove/commit/d86e0500c09c3714dc424cce8f30827cb4b240d3))

<a name="0.1.0-alpha.2"></a>
## [0.1.0-alpha.2](https://www.github.com/tgiachi/Eldergrove/releases/tag/v0.1.0-alpha.2) (2024-10-09)

### Features

* **AutostartServiceAttribute:** set default order value to 5 for better usability ([cc7a5ae](https://www.github.com/tgiachi/Eldergrove/commit/cc7a5aed74c5f70e3ff6297f2235baccbc6ce469))
* **DoorComponent:** add isOpen parameter to constructor for better door state management ([132d4db](https://www.github.com/tgiachi/Eldergrove/commit/132d4db7743b054b5f648ee09b2b246467bcacf9))
* **ItemsContainer:** implement ItemsContainerComponent to manage items in game objects ([531253a](https://www.github.com/tgiachi/Eldergrove/commit/531253a5c3e5b2932f912e7c0e65d6f4f4b5fb57))

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

