return function()
    add_tile({
        id = "t_tile",
        symbol = "T",
    })

    add_tile({
        id = "t_wall",
        symbol = "#",
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
end
