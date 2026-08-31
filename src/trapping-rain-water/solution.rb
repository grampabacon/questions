# frozen_string_literal: true

def trap(height)
  left = 0
  right = height.length - 1

  left_max = 0
  right_max = 0

  result = 0

  while left <= right
    if height[left] <= height[right]
      if height[left] >= left_max
        left_max = height[left]
      else
        result += left_max - height[left]
      end

      left += 1
    else
      if height[right] >= right_max
        right_max = height[right]
      else
        result += right_max - height[right]
      end

      right -= 1
    end
  end

  result
end

def check(height, expected)
  result = trap(height)
  raise "Expected #{expected} ~ Actual #{result}" if result != expected
end

check([0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1], 6)
check([4, 2, 0, 3, 2, 5], 9)
check([4, 2, 3], 1)
puts 'All tests passed.'
