char_depth = 0.0001;

char_pixelsz = 0.02/5;
char_pheight = 5;
char_max_pwidth = 5;

function sum(list) = list * [for (_=list) 1];
function partialsum(list, end) = list * [for (i=[0:len(list)-1]) i < end ? 1 : 0];

module _Text_Line(a, b) {
  l = norm(b-a);
  f = l > 0 ? (b-a)/l : [1, 0];
  r = [f.y, -f.x];
  ea = -0.5*f;
  eb = (l+0.5)*f;
  sr = 0.5*r;
  sl = -sr;
  polygon([
    a+ea+sr,
    a+eb+sr,
    a+eb+sl,
    a+ea+sl,
  ]*char_pixelsz);
}

module _Text_Segments(pts, off=[0,0]) {
  for (i=[0:len(pts)-2]) {
    pa = pts[i];
    pb = pts[i+1];
    if (len(pa) == 2 && len(pb) == 2)
      _Text_Line(pts[i]+off, pts[i+1]+off);
  }
}

_Letters =
let(
  bl=[-2,-2],
  bc=[0,-2],
  br=[2,-2],
  cl=[-2,0],
  cc=[0,0],
  cr=[2,0],
  tl=[-2,2],
  tc=[0,2],
  tr=[2,2],
  _=[],
  hx=[0.5,0],
  hy=[0,0.5],
  x=[1,0],
  y=[0,1]
) [
  [bl, tl, tr, br, _, cl, cr], // A
  [cr+hy, br, bl, tl, tc+hx, _, cl, cr, _, tc+y, cr+x], // B
  [br, bl, tl, tr], // C
  [cr+hy, br, bl, tl, tc+hx, _, cr+x, tc+y], // D
  [br, bl, tl, tr, _, cl, cc], // E
  [tr, tl, bl, cl, cc], // F
  [cc, cr, br, bl, tl, tr], // G
  [bl, tl, cl, cr, tr, br], // H
  [bc, tc], // I
  [cl, bl, br, tr], // J
  [bl, tl, _, cl, cc, _, tr+hx+hy, cc, br+hx-hy], // K
  [tl, bl, br], // L
  [bl, tl, tr, br, _, bc, tc], // M
  [bl, tl, tr, br], // N
  [bl, tl, tr, br, bl], // O
  [bl, tl, tr, cr, cl], // P
  [bl, tl, tr, br, bl, _, cc+hx-hy, br], // Q
  [bl, tl, tr, cr, cl, bc+hx-hy], // R
  [bl, br, cr, cl, tl, tr], // S
  [tl, tr, _, bc, tc], // T
  [tl, bl, br, tr], // U
  [tl, cl, _, tr, cr, _, cl-x+hy, bc-hy, cr+x+hy], // V
  [tl, bl, br, tr, _, bc, tc], // W
  [bl-hx-hy, tr+hx+hy, _, tl-hx+hy, br+hx-hy], // X
  [tl, cl, cr, tr, _, bc, cc], // Y
  [br, bl+hx, _, bl-hx-hy, tr+hx+hy, _, tr-hx, tl], // Z
  [bl, tl, tr, br, bl, _, bl, tr], // 0
  [bc, tc, tc-x], // 1
  [tl, tr, cr, cl, bl, br], // 2
  [bl, br, tr, tl, _, cc, cr], // 3
  [tl, cl, cr, tr, br], // 4
  [tr, tl, cl, cr, cr-hy, _, cr+x, bc-y, _, bc+hx, bl], // 5
  [tr, tl, bl, br, cr, cl], // 6
  [tl, tr, br], // 7
  [bl, tl, tr, br, bl, cl, cr], // 8
  [br, tr, tl, cl, cr], // 9
  [tl, tr, cr, cc, _, bc, bc], // ? for missing
];

function _Letter_Lookup(c) =
  let(
    ci=ord(c),
    li=ci-ord("A"),
    ni=ci-ord("0"),
    issp=ci==32,
    lps=li>=0&&li<26 ? _Letters[li] : ni>=0&&ni<10 ? _Letters[ni+26] : [],
    lpf=len(lps)==0 ? _Letters[36] : lps
  ) issp ? [] : lpf;

function _Letter_Bounds(letter) =
  let(
    fletter = concat([[0, 0]], [for (l=letter) len(l)==2 ? l : [0,0]]),
    xs = [for (l=fletter) l.x],
    ys = [for (l=fletter) l.y],
    h_edge = (char_max_pwidth - 1) / 2,
    v_edge = (char_pheight - 1) / 2
  ) [
    max(min(xs), -h_edge),
    max(min(ys), -v_edge),
    min(max(xs), h_edge),
    min(max(ys), v_edge)
  ];

module Text(text) {
  letters = [for (c=text) _Letter_Lookup(c)];
  lbounds = [for (l=letters) _Letter_Bounds(l)];
  lwidths = [for (b=lbounds) min(b.z-b.x+1, char_max_pwidth)];
  lmins = [for (b=lbounds) b.x];
  twid = sum(lwidths) + len(letters)-1;
  startoff = (1-twid)/2;
  offs = [for (i=[0:len(letters)-1]) startoff+i+partialsum(lwidths, i)];
  linear_extrude(height=char_depth) {
    for (i=[0:len(letters)-1]) if (len(letters[i]) > 0) {
      letter = letters[i];
      off = offs[i];
      left = off-lmins[i];
      wid = lwidths[i];
      intersection() {
        _Text_Segments(letter, [left, 0]);
        scale([char_pixelsz,char_pixelsz]) translate([off-0.5, -char_pheight/2])
          square(size=[wid, char_pheight]);
      }
    }
  }
}

module TextLines(lines, spacing=3) {
  offset = (spacing+char_pheight)*char_pixelsz;
  totaloff = (len(lines)-1)*offset;
  first_off = totaloff/2;
  offs = [for (i=[0:len(lines)-1]) first_off-offset*i];
  for (i=[0:len(lines)-1]) translate([0,offs[i],0]) Text(lines[i]);
}