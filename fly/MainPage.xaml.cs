namespace fly;

public partial class MainPage : ContentPage
{
	const int TempoEntreFrames = 25;
	const int gravidade = 3;
	double LarguraJanela;
	double AlturaJanela;
	int velocidade = 20;
	bool estaMorto = true;
	public MainPage()
	{
		InitializeComponent();
	}
	public async void Desenha()
	{
		while (!estaMorto)
		{
			AplicaGravidade();
			GerenciaCanos();
			await Task.Delay(TempoEntreFrames);
		}
	}
	void AplicaGravidade()
	{
		Passaro.TranslationY += Gravidade:
		}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		LarguraJanela = w;
		AlturaJanela = h;

	}
	void GerenciaCanos()
	{
		imgCanoCima.TransltionX -= velocidade;
		imgCanoBaixo.TransltionX -= velocidade;

		if ()
}