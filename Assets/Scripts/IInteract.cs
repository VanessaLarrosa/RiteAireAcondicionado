public interface IInteract
{
    void Interact();
    void GetName();
    void Completed(bool value);

    // Nuevos para detección de ratón
    void OnHoverEnter();
    void OnHoverExit();
}
