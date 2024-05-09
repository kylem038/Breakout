shader_type canvas_item;

uniform float speed : hint_range(0.0, 10.0) = 0.5;

void fragment() {
	vec2 shifted_uv = vec2(UV.x + TIME * speed, UV.y);
	float value = 0.5 + 0.5 * sin(TIME * speed);
	float opposite_value = 1.0 - value;
	COLOR = vec4(UV.x, UV.y, value, 1.0);
}