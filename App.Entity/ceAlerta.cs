using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Entity
{
    public class CeAlerta
    {
        public string ID        { get; set; }
        public string Nombre    { get; set; }
        public string IP        { get; set; }
        public string Mac       { get; set; }
        public string Placa     { get; set; }
        public string Fecha     { get; set; }
        public byte[] ImgPlaca  { get; set; }
        public byte[] ImgAuto   { get; set; }
        public float Latitud    { get; set; }
        public float Longitud   { get; set; }
        public float Velocidad  { get; set; }
        public string Arco      { get; set; }
        public string Carril    { get; set; }
        public string Sentido   { get; set; }
        public byte[] Buffer    { get; set; }
        public uint Archivo     { get; set; }
        public uint Info        { get; set; }        
    }

    public class CeAlertaL
    {
        public string ID        { get; set; }
        public string Nombre    { get; set; }
        public string IP        { get; set; }
        public string Mac       { get; set; }
        public string Placa     { get; set; }
        public string Fecha     { get; set; }
        public byte[] ImgPlaca  { get; set; }
        public byte[] ImgAuto   { get; set; }
        public float Latitud    { get; set; }
        public float Longitud   { get; set; }
        public float Velocidad  { get; set; }
        public string Arco      { get; set; }
        public string Carril    { get; set; }
        public string Sentido   { get; set; }
        public byte[] Buffer    { get; set; }
        public uint Archivo     { get; set; }
        public uint Info        { get; set; }
    }

    public class CeAlertaN
    {
        public string ID        { get; set; }
        public string Nombre    { get; set; }
        public string IP        { get; set; }
        public string Mac       { get; set; }
        public string Placa     { get; set; }
        public string Fecha     { get; set; }
        public byte[] ImgPlaca  { get; set; }
        public byte[] ImgAuto   { get; set; }
        public float Latitud    { get; set; }
        public float Longitud   { get; set; }
        public float Velocidad  { get; set; }
        public string Arco      { get; set; }
        public string Carril    { get; set; }
        public string Sentido   { get; set; }
        public byte[] Buffer    { get; set; }
        public uint Archivo     { get; set; }
        public uint Info        { get; set; }
    }
}
