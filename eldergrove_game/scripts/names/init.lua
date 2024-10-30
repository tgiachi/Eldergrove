-- Add names from files to the name generator
return function ()
    name_add_from_file("male", "male_names.txt")
    name_add_from_file("female", "female_names.txt")
    name_add_from_file("animal", "animals_names.txt")
end
