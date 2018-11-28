namespace Polyworks {
	using System; 

	public interface IAnimatable {
		void Play(string clip);
		void Pause();
		void Resume();
		bool GetIsActive();
	}
	
}