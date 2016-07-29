# CSharpDemoFastGuidedFilter
The guided filter is a technique for edge-aware image filtering. I implement fast kind of the guided filter as fast guided filter in Aforge.NET framework and use it to demonstrate how to usage in a sample C# code.
I spend a couple of days to implementing 'Multiply', 'Divide', 'FastBoxBlur' and ['FastGuidedFilter'](http://arxiv.org/abs/1505.00996) filters that's added to forked [accord.net](https://github.com/hzawary/accord-net) repository, while it's implemented on official OpenCV and Matlab.

Result of 'FastGuidedFilters' enhancement using AForge.Imaging.Filters.FastGuidedFilter class (Original image in left side and filtered on right side).
![image](https://cloud.githubusercontent.com/assets/13867408/13763185/34fde4ee-ea3c-11e5-941b-5e5bcefbf0d6.png)
