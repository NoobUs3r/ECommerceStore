using InfrastructureService.Repositories;
using InfrastructureService.Services;
using SharedService.Interfaces;
using SharedService.Models;

namespace InfrastructureFactory
{
    public class InfrastructureFactory : IInfrastructureFactory
    {
        private IAuthService? _authService;
        private IUserRepository? _userRepository;
        private IProductRepository? _productRepository;
        private IPasswordHasher? _passwordHasher;
        private ITokenService? _tokenService;

        public IProductRepository CreateProductRepository()
        {
            return _productRepository ??= new ProductRepository(new InMemoryDatabase<Product>());
        }

        public IUserRepository CreateUserRepository()
        {
            return _userRepository ??= new UserRepository(new InMemoryDatabase<User>());
        }

        public IAuthService CreateAuth(string jwtKey, string jwtIssuer, string jwtAudience)
        {
            _userRepository ??= CreateUserRepository();
            _passwordHasher ??= new PasswordHasher();
            _tokenService ??= new TokenService(jwtKey, jwtIssuer, jwtAudience);

            return _authService ??= new AuthService(_userRepository, _passwordHasher, _tokenService);
        }
    }
}