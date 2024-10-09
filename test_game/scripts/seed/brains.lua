
local brains = {}


local function dummy_brain() 

    log_info("Dummy brain called")
end


function brains.seed()
    add_brain("dummy", dummy_brain)
end

return brains