use <text.scad>;

smallSize = 0.5/8;
padding = 0.01;
function smallScale(size) = (smallSize * size - padding/2) / 0.02;

scales = [
  20,
  smallScale(1),
  smallScale(2),
  smallScale(3),
];
letters = ["A", "B", "C"];

idx = round($t*12);
lscale = scales[floor(idx)/3];
letter = letters[idx%3];

scale(lscale) Text(letter);