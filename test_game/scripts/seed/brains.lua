
local brains = {}


local function dummy_brain() 

end


function brains.seed()
    add_brain("dummy", dummy_brain)
end

return brains