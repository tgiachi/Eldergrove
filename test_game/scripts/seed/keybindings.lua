return function ()
    register_keybinding("map", "UP", "player_move_up")
    register_keybinding("map", "DOWN", "player_move_down")
    register_keybinding("map", "LEFT", "player_move_left")
    register_keybinding("map", "RIGHT", "player_move_right")


    register_keybinding("map", "F1", "toggle_debug")


    action_register_cmd("toggle_debug", function(ctx)
         ctx:GetEngine():GetNpcService():GetPlayer():ShowAllMap()
    end)

end
