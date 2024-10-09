local npc_seed = {}

function npc_seed.seed()
    add_npc({
        id = "a_cat",
        name = "@animals",
        symbol = "#@c",
        foreground = "black",
        background = "white",
        category = "animals",
        sub_category = "cats",
        skills = {
            health = {
                min = 10,
                max = 20
            }
        }
    })

   local cat =  build_npc("animals", 1, 1)
    log_info(cat)
end

return npc_seed