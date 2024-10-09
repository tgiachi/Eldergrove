local npc_seed = {}

function npc_seed.seed()
    add_npc({
        id = "a_cat",
        name = "@animals",
        symbol = "#c",
        foreground = "black",
        background = "white",
        category = "animals",
        sub_category = "cats",
    })

    build_npc("animals", 1, 1)
end

return npc_seed