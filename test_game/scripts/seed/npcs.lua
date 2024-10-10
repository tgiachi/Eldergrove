return function()
    add_npc({
        id = "a_cat",
        name = "@animals",
        symbol = "##c",
        foreground = "black",
        background = "white",
        category = "animals",
        sub_category = "cats",
        brain_ai = "dummy",
        skills = {
            health = {
                min = 10,
                max = 20
            }
        }
    })

    local cat = build_npc("animals", 1, 1)
    log_info(cat)
end
