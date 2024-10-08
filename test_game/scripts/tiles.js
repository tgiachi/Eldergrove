"use strict";
log_info("Adding tiles");
add_tile({
    id: "floor",
    tags: ["floor"],
    symbol: ".",
    foreground: "dgray",
    background: "black",
    is_blocking: false,
    is_transparent: true
});
add_tile({
    id: "wall",
    tags: ["wall"],
    symbol: "#",
    foreground: "dgray",
    background: "black",
    is_blocking: true,
    is_transparent: false
});
