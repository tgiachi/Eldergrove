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

    add_npc({
        id = "a_goblin",
        name = "@male",
        symbol = "##g",
        foreground = "black",
        background = "green",
        category = "monsters",
        sub_category = "goblins",
        brain_ai = "empty",
        skills = {
            health = {
                min = 10,
                max = 20
            }
        }
    })
end
