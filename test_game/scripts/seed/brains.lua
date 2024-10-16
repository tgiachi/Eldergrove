local function dummy_brain(ctx)
    return ctx:DoMovement(ctx:GoRandom())
end

local function empty_brain(ctx)
    return ctx:EmptyActionList()
end




return function ()
    add_brain("dummy", dummy_brain)
    add_brain("empty", empty_brain)
end
