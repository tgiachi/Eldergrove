local color_seed = require "seed.colors"
local tile_seed = require "seed.tiles"
local name_seed = require "seed.names"
local npc_seed = require "seed.npcs"
local brain_seed = require "seed.brains"
local keybinding = require "seed.keybindings"

game_config({
    title_name = "Eldergrove",
    map = {
        generator_id = "rooms",
        width = 400,
        height = 400
    },
    player = {
        starting_gold = {
            min = 100,
            max = 200
        }
    },
    scheduler = {
        is_turn_based = true

    },
    fonts = {
       -- gui_font = "Fonts/mdcurses16.font",
        --map_font = "Fonts/Curses.font"
        map_font = "Fonts/mdcurses16.font"

    }
})

on_bootstrap(function()
    color_seed()
    tile_seed()
    name_seed()
    brain_seed()
    npc_seed()

    keybinding()
end)


on_ready(function()
    log_info("Game is ready")
    generate_map()
end)

