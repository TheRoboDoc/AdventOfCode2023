from collections import Counter


def read_file() -> str:
    with open("input.txt") as file:
        data: str = file.read().strip()

    return data


def get_hand_type(hand: str) -> int:
    counter: Counter[str] = Counter(hand)

    counts: list[int] = [0] if (jokers := counter.pop("*", 0)) == 5 else sorted(counter.values())

    counts[-1] += jokers

    match counts:
        case *_, 5:
            return 7
        case *_, 4:
            return 6
        case *_, 2, 3:
            return 5
        case *_, 3:
            return 4
        case *_, 2, 2:
            return 3
        case *_, 2:
            return 2

    return 1


if __name__ == '__main__':
    file_input = read_file().replace("J", "*")

    win_set: list[list[str]] = [line.split() for line in file_input.split("\n")]

    winning: int = sum(
        int(rank) * int(bid)

        for rank, (*_, bid) in enumerate(
            sorted(
                (get_hand_type(str(hand)), *map("*23456789TJQKA".index, str(hand)), int(bid))
                for hand, bid in win_set
            ),
            1,
        )
    )

    print("Total Winnings: ", winning)
