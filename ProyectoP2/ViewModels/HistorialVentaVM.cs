﻿using ProyectoP2.DataAccess;
using ProyectoP2.DTOs;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System;

namespace ProyectoP2.ViewModels
{
    public partial class HistorialVentaVM : ObservableObject
    {
        private readonly VentaDbContext _context;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private ObservableCollection<VentaDTO> listaVenta = new();

        public HistorialVentaVM(VentaDbContext context)
        {
            _context = context;
            Task.Run(ObtenerVentas);
        }

        private async Task ObtenerVentas()
        {
            IsLoading = true;
            var lista = await _context.Ventas.OrderByDescending(v => v.IdVenta).ToListAsync();

            if (lista.Any())
            {
                foreach (var item in lista)
                {
                    ListaVenta.Add(new VentaDTO
                    {
                        Cliente = item.Cliente,
                        NumeroVenta = item.NumeroVenta,
                        Total = item.Total,
                        PagoCon = item.PagoCon,
                        Cambio = item.Cambio,
                        FechaRegistro = item.FechaRegistro.ToString("dd/MM/yyyy")
                    });
                }
            }
            IsLoading = false;
        }

        public ICommand DescargarVentaCommand => new Command<VentaDTO>(async (venta) => await DescargarVentaAsync(venta));

        private async Task DescargarVentaAsync(VentaDTO venta)
        {
            try
            {
                var ventasText = GenerarTextoVenta(venta);

                var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var filePath = Path.Combine(desktopPath, $"venta_{venta.NumeroVenta}_{DateTime.Now:yyyyMMdd_HHmmss}.txt");

                File.WriteAllText(filePath, ventasText);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Compartir Detalles de Venta",
                    File = new ShareFile(filePath)
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private string GenerarTextoVenta(VentaDTO venta)
        {
            var ventaText = $"Detalles de la Venta {venta.NumeroVenta}\n\n";
            ventaText += $"Cliente: {venta.Cliente}\n";
            ventaText += $"Pagado con: {venta.PagoCon}\n";
            ventaText += $"Cambio: {venta.Cambio}\n";
            ventaText += $"Fecha: {venta.FechaRegistro}\n";
            ventaText += $"Total: {venta.Total}\n";

            return ventaText;
        }
    }
}
