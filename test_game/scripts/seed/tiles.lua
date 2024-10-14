return function()

    add_tile({
        id = "t_player",
        symbol = "##@",
        foreground = "lred",
    })

    add_tile({
        id = "t_tile",
        symbol = "T",
    })

    add_tile({
        id = "t_window",
        symbol = "w",
        background = "black",
        foreground = "cyan",
    })

    add_tile({
        id = "t_wall",
        symbol = "!!178",
        background = "black",
        foreground = "white",
        is_blocking = true,
        is_transparent = false,
    })

    add_tile({
        id = "t_floor",
        symbol = "`",
        background = "black",
        foreground = "white",
        is_blocking = false,
        is_transparent = true,
    })

    test_animation = {
        id = "t_flame",
        symbol = "f",
        background = "black",
        foreground = "white",
        is_blocking = false,
        is_transparent = true,
        animation = {
            starting = {
                symbol = "f",
                background = "#ff4500",
                foreground = "#ff8c00",
            }
        }
    }


    test_animation["animation"]["ending"] = {
        symbol = "F",
        background = "#ffd700",
        foreground = "#ffffff",
    }
    add_tile(test_animation)
end
