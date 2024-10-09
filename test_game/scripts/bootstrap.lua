local my_module = require "test_mod.index"


local function bootstrap()
    log_info("bootstrap")

    my_module.test_func()

    add_tile({
        id = "t_tile",
        symbol = "T",
    })
end


bootstrap()
