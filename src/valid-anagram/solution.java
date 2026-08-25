import java.util.HashMap;
import java.util.Map;

class Solution {

  static boolean isAnagram(String s, String t) {
    if (s.length() != t.length()) {
      return false;
    }

    Map<Character, Integer> counts = new HashMap<>();

    for (char c : s.toCharArray()) {
      counts.merge(c, 1, Integer::sum);
    }

    for (char c : t.toCharArray()) {
      Integer count = counts.get(c);
      if (count == null || count == 0) {
        return false;
      }
      counts.put(c, count - 1);
    }

    return true;
  }

  static void check(boolean actual, boolean expected, String label) {
    if (actual != expected) {
      throw new RuntimeException(
          "Test failed (" + label + "): got " + actual + ", expected " + expected);
    }
  }

  static void main(String[] args) {
    check(isAnagram("anagram", "nagaram"), true, "anagram/nagaram");
    check(isAnagram("rat", "car"), false, "rat/car");
    System.out.println("All tests passed.");
  }
}
