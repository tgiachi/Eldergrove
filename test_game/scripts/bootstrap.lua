local color_seed = require "seed.colors"
local tile_seed = require "seed.tiles"
local name_seed = require "seed.names"
local npc_seed = require "seed.npcs"
local brain_seed = require "seed.brains"

set_game_config({
    title_name = "Eldergrove",
    map = {
        generator_id = "default",
        width = 400,
        height = 400
    }
})

on_bootstrap(function()
    color_seed()
    tile_seed()
    name_seed()
    brain_seed()
    npc_seed()
end)


on_ready(function()
    log_info("Game is ready")
    generate_map()
end)
