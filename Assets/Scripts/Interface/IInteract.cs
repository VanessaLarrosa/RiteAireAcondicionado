public interface IInteract
{
    void Interact();
    void GetName();
    void Completed(bool value);

    // Nuevos para detecci�n de rat�n
    void OnHoverEnter();
    void OnHoverExit();
}
