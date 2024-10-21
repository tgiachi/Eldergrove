local function dummy_brain(ctx)
    return ctx:DoMovement(ctx:GoRandom())
end

local function empty_brain(ctx)

end


local function goblin_brain(ctx)

    if (ctx:IsPlayerInRange(50)) then
        return ctx:DoMovement(ctx:GoRandom())
    end

    return ctx:EmptyActionList()

end



return function ()
    add_brain("dummy", dummy_brain)
    add_brain("empty", empty_brain)
    add_brain("goblin", goblin_brain)
end
