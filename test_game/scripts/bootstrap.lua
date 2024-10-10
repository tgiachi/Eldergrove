local color_seed = require "seed.colors"
local tile_seed = require "seed.tiles"
local name_seed = require "seed.names"
local npc_seed = require "seed.npcs"
local brain_seed = require "seed.brains"

set_game_config({
    map_width = 300,
    map_height = 300,
})

on_bootstrap(function()
    color_seed.colors()
    tile_seed.seed()
    name_seed.seed()
    brain_seed()
    npc_seed.seed()
end)


on_ready(function()
    log_info("Game is ready")
    generate_map()
end)

