local names = require "names.init"
local brains = require "ai.init"


game_config({
    title_name = "Eldergrove",
    fonts = {
        map_font = "Tangaria32.font"
    },
    player = {
        starting_gold = {
            min = 100,
            max = 200
        }
    }
})

on_bootstrap(function()
    names()
    brains()
end)
