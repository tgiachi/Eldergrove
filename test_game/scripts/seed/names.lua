local names_seed = {}

function names_seed.seed()
    name_add_from_file("animals", "names/fantasy_animal_names.txt")
    name_add_from_file("female", "names/fantasy_female_names.txt")
    name_add_from_file("male", "names/fantasy_male_names.txt")
end


return names_seed