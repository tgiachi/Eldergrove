-- Eldergrove Engine Lua Definitions

-- game_config: any
-- bootstrap: any

function log_info(message, args) end

function log_debug(message, args) end

function log_warn(message, args) end

function log_error(message, args) end

-- Sends a message log event
function message_log(message, messageLogType) end

function add_ctx(name, value) end

-- Load json file
function load_json(path) end

function on_bootstrap( callBack ) end

-- Set game config
function game_config(value) end

-- Generate lua definitions
function gen_lua_def() end

function action_register_cmd(command, action) end

function action_unregister_cmd(command) end

function action_execute_cmd(command) end

function register_keybinding(context, key, command) end

function name_add(type, name) end

function name_add_from_file(type, path) end

function name_generate(type) end

function add_color(colorName, value) end

-- Returns a random integer value.
function rnd_int(min, max) end

-- Rolls a dice expression.
function rnd_dice(expression) end

-- Returns a random boolean value.
function rnd_bool() end

function task(action) end

function dispatch_event(eventName, eventData) end

function sub_event(eventName, eventHandler) end

function on_ready(eventHandler) end

function add_tile(tileData) end

-- Adds an npc to the game
function add_npc(npcData) end

-- Builds an npc game object
function build_npc(idOrCategory, x, y) end

-- Adds a brain to an npc
function add_brain(id, brain) end

function generate_map() end

function add_var(variableName, builder) end

function add_var(variableName, value) end

function translate(text) end

function add_text(id, text) end

function get_text(id) end

-- Makes a GET request to the specified URL and returns the response.
function http_get(url) end

-- Converts a JSON string to an object.
function from_json(json) end

-- Adds an item feature to the game
function add_item_feature(featureName, callBack) end

