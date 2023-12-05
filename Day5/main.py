import re
from re import Pattern, Match
from concurrent.futures import ProcessPoolExecutor


def parse_map(map_content: Match) -> list[list[int]]:
    map_lines = map_content.strip().split('\n')

    parsed_map = [list(map(int, line.split())) for line in map_lines]

    return parsed_map


def convert_number(number: int, single_map: dict) -> int:
    converted_number: int = number

    for destination_start, source_start, length in single_map:
        source_end = source_start + length

        if source_start <= number < source_end:
            difference = number - source_start

            converted_number = destination_start + difference
            break

    return converted_number


def find_location_number(seed: int, _maps_order: list[str], maps_dict: dict) -> int:
    current_number: int = seed

    for map_category in _maps_order:
        current_number: int = convert_number(current_number, maps_dict[map_category])

    return current_number


def process_seed_range(start: int, length: int, _maps_order: list[str], maps_dict: dict) -> int:
    lowest_location_for_range = float('inf')

    for seed in range(start, start + length):
        location_number = find_location_number(seed, _maps_order, maps_dict)
        lowest_location_for_range = min(lowest_location_for_range, location_number)

    return lowest_location_for_range


def find_lowest_location_from_ranges(seed_range_line: Match, _maps_order: list[str], maps_dict: dict) -> int:
    values = list(map(int, seed_range_line.strip().split()))

    seed_ranges = [(values[i], values[i + 1]) for i in range(0, len(values), 2)]

    with ProcessPoolExecutor() as executor:
        futures = [executor.submit(process_seed_range, start, length, _maps_order, maps_dict) for start, length in
                   seed_ranges]

        lowest_location_numbers = [future.result() for future in futures]

    return min(lowest_location_numbers)


if __name__ == '__main__':
    txt_file_path: str = 'Almanac.txt'

    with open(txt_file_path, 'r') as file:
        txt_content: str = file.read()

    seed_pattern: Pattern[str] = re.compile(r'seeds:\s+([\d\s]+)')
    map_pattern: Pattern[str] = re.compile(r'(\w+-to-\w+ map:)([\s\S]+?)(?=\w+-to-\w+ map:|\Z)')

    seed_matches: list[Match] = seed_pattern.findall(txt_content)
    map_matches: list[Match] = map_pattern.findall(txt_content)

    initial_seeds: Match = seed_matches[0] if seed_matches else None
    first_map_title, first_map_content = map_matches[0] if map_matches else (None, None)

    all_map_matches: list[Match] = map_pattern.findall(txt_content)

    maps: dict[Match, Match] = {match[0]: match[1] for match in all_map_matches}

    initial_seeds_list: list[int] = list(map(int, initial_seeds.strip().split()))

    parsed_maps: dict = {key.replace(' map:', ''): parse_map(value) for key, value in maps.items()}

    maps_order: list[str] = ['seed-to-soil',
                             'soil-to-fertilizer',
                             'fertilizer-to-water',
                             'water-to-light',
                             'light-to-temperature',
                             'temperature-to-humidity',
                             'humidity-to-location']

    location_numbers: list[int] = [find_location_number(
        seed, maps_order, parsed_maps) for seed in initial_seeds_list]

    lowest_location_number: int = min(location_numbers)

    print("Answer Part 1: " + str(lowest_location_number))

    lowest_location_number: int = find_lowest_location_from_ranges(seed_matches[0],
                                                                   maps_order,
                                                                   parsed_maps)

    print("Answer Part 2: " + str(lowest_location_number))
