


local function dummy_brain() 

    log_info("Dummy brain called")
end




return function ()
    add_brain("dummy", dummy_brain)
end