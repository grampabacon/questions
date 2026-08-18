def is_anagram(s: str, t: str) -> bool:
    if len(s) != len(t):
        return False

    characters: dict[str, int] = {}
    for c in s:
        if c in characters:
            characters[c] += 1
        else:
            characters[c] = 1

    for c in t:
        if c not in characters:
            return False

        characters[c] -= 1

        if characters[c] == 0:
            del characters[c]

    if len(characters) == 0:
        return True

    return False


if __name__ == "__main__":
    assert is_anagram("anagram", "nagaram") is True
    assert is_anagram("rat", "car") is False
    print("All tests passed.")
