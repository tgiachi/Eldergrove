


local function dummy_brain(ctx)

    log_info(ctx)
    log_info("Dummy brain called")

    return ctx:EmptyActionList();
end




return function ()
    add_brain("dummy", dummy_brain)
end
