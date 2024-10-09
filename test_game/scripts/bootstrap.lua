local color_seed = require "seed.colors"
local tile_seed = require "seed.tiles"
local name_seed = require "seed.names"
local function bootstrap()
    color_seed.colors()
    tile_seed.seed()
    name_seed.seed()
end


bootstrap()
