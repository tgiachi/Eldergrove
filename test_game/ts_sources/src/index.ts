
const titles = () => {
  log_info("Adding tiles");

  add_tile({
    id: "floor",
    tags: ["floor"],
    symbol: ".",
    foreground: "dgray",
    background: "black",
    is_blocking: false,
    is_transparent: true
  })

  add_tile({
    id: "wall",
    tags: ["wall"],
    symbol: "#",
    foreground: "dgray",
    background: "black",
    is_blocking: true,
    is_transparent: false
  })
}


const bootstrap = () => {

  log_info("Adding colors");
  add_color("black", [19, 32, 46]);
  add_color("dgray", [97, 97, 112]);
  add_color("gray", [93, 142, 142]);
  add_color("white", [223, 191, 127]);
  add_color("brown", [112, 74, 74]);
  add_color("yellow", [207, 143, 48]);
  add_color("red", [159, 32, 64]);
  add_color("lred", [191, 80, 64]);
  add_color("green", [140, 140, 83]);
  add_color("lgreen", [163, 175, 99]);
  add_color("blue", [64, 96, 93]);
  add_color("lblue", [127, 143, 159]);
  add_color("magenta", [156, 16, 99]);
  add_color("lmagenta", [214, 66, 130]);
  add_color("cyan", [73, 73, 136]);
  add_color("lcyan", [112, 112, 143]);

  name_add_from_file("female", "names/fantasy_female_names.txt");
  name_add_from_file("male", "names/fantasy_male_names.txt");
  name_add_from_file("animal", "names/fantasy_animal_names.txt");

  titles();

}


bootstrap();