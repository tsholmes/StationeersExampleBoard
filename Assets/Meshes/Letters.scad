use <text.scad>;

if ($t < 0.5) {
  scale(20) Text("A");
} else {
  scale(2) Text("B");
}