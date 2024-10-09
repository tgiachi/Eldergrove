local color_seed = require "seed.colors"
local tile_seed = require "seed.tiles"
local name_seed = require "seed.names"
local npc_seed = require "seed.npcs"

local function bootstrap()
    color_seed.colors()
    tile_seed.seed()
    name_seed.seed()
    npc_seed.seed()
end


bootstrap()
