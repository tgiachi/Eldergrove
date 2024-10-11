


local function dummy_brain(ctx)

    return ctx:DoMovement(ctx:GoRandom())
end




return function ()
    add_brain("dummy", dummy_brain)
end
