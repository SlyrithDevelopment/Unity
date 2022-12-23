public static float GetAudioClipLength(AudioClip clip) {
	if (clip == null) return 0f;
	float clipSampleLength = clip.frequency <= 0f ? 0f : clip.samples / clip.frequency;
	return Mathf.Max(clip.length, clipSampleLength);
}
