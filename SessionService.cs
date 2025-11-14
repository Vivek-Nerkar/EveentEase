// Services/SessionService.cs
using EventEase.Models;
using Microsoft.JSInterop;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace EventEase.Services
{
    public class SessionService : ISessionService
    {
        private const string StorageKey = "eventease_session_v1";
        private readonly IServiceProvider _services;
        private Session? _cached;

        public SessionService(IServiceProvider services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        // safe resolver — returns null if JS runtime unavailable (e.g., during server prerender)
        private IJSRuntime? ResolveJs()
        {
            try
            {
                return _services.GetService<IJSRuntime>();
            }
            catch
            {
                return null;
            }
        }

        public async Task<Session?> GetCurrentAsync()
        {
            if (_cached != null) return _cached;

            var js = ResolveJs();
            if (js == null) return null;

            try
            {
                var json = await js.InvokeAsync<string?>("storageHelper.get", StorageKey);
                if (string.IsNullOrEmpty(json)) return null;
                _cached = JsonSerializer.Deserialize<Session>(json);
                return _cached;
            }
            catch
            {
                // if JS fails (e.g., prerender), return null
                return null;
            }
        }

        public async Task SaveAsync(Session session)
        {
            if (session == null) throw new ArgumentNullException(nameof(session));
            session.LastSeen = DateTime.UtcNow;
            _cached = session;

            var js = ResolveJs();
            if (js == null) return; // can't persist during prerender; skip

            try
            {
                var json = JsonSerializer.Serialize(session);
                await js.InvokeVoidAsync("storageHelper.set", StorageKey, json);
            }
            catch
            {
                // ignore JS failures
            }
        }

        public async Task ClearAsync()
        {
            _cached = null;
            var js = ResolveJs();
            if (js == null) return;

            try
            {
                await js.InvokeVoidAsync("storageHelper.remove", StorageKey);
            }
            catch
            {
            }
        }

        public async Task TouchAsync()
        {
            if (_cached == null) return;
            _cached.LastSeen = DateTime.UtcNow;

            var js = ResolveJs();
            if (js == null) return;

            try
            {
                var json = JsonSerializer.Serialize(_cached);
                await js.InvokeVoidAsync("storageHelper.set", StorageKey, json);
            }
            catch
            {
            }
        }
    }
}
