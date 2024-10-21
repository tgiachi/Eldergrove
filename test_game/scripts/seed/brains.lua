local function dummy_brain(ctx)
    return ctx:DoMovement(ctx:GoRandom())
end

local function empty_brain(ctx)
    return ctx:EmptyActionList()
end


local function goblin_brain(ctx)
    return ctx:DoMovement(ctx:GoRandom())
end



return function ()
    add_brain("dummy", dummy_brain)
    add_brain("empty", empty_brain)
    add_brain("goblin", goblin_brain)
end
