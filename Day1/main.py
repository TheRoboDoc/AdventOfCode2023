import re


NUMBER_PAIRS: dict[str, int] = {
    'one': 1,
    'two': 2,
    'three': 3,
    'four': 4,
    'five': 5,
    'six': 6,
    'seven': 7,
    'eight': 8,
    'nine': 9,
}


def get_calibration_values() -> list[str]:
    _values: list[str] = []

    with open('calibrationvalues.txt', 'r') as _file:
        for _line in _file:
            _values.append(_line)

    return _values


def get_numbers(_values: list[str]) -> list[int]:
    _numbers: list[int] = []

    for _value in _values:
        _first: str = ''
        _last: str = ''

        _digits = re.findall(r'(?=(one|two|three|four|five|six|seven|eight|nine|\d))', _value)

        if _digits[0] in NUMBER_PAIRS:
            _first = str(NUMBER_PAIRS[_digits[0]])
        else:
            _first = _digits[0]

        if _digits[-1] in NUMBER_PAIRS:
            _last = str(NUMBER_PAIRS[_digits[-1]])
        else:
            _last = _digits[-1]

        _numbers.append(int(_first + _last))

    return _numbers


def sum_values(_values: list[int]) -> int:
    _sum: int = 0

    for _value in _values:
        _sum += _value

    return _sum


if __name__ == '__main__':
    values: list[str] = get_calibration_values()
    numbers: list[int] = get_numbers(values)
    answer: int = sum_values(numbers)

    print(answer)
