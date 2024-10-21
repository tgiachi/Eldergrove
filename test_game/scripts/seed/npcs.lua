return function()


    add_npc({
        id = "a_goblin",
        name = "@male",
        symbol = "##g",
        foreground = "black",
        background = "green",
        category = "monsters",
        sub_category = "goblins",
        brain_ai = "goblin",
        skills = {
            health = {
                min = 10,
                max = 20,
            },
            gold = {
                min = 100,
                max = 10000
            }
        }
    })
end
