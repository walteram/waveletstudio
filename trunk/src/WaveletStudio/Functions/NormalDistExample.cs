/////////////////////////////////////////////////////////////////////////////
// Copyright (c) 2003 CenterSpace Software, LLC                            //
//                                                                         //
// This code is free software under the Artistic license.                  //
//                                                                         //
// CenterSpace Software                                                    //
// 260 SW Madison Avenue                                                   //
// Suite #112                                                              //
// Corvallis, Oregon, 97333                                                //
// USA                                                                     //
// http://www.centerspace.net                                              //
/////////////////////////////////////////////////////////////////////////////


using System;

namespace CenterSpace.Free
{
  /// <summary>
  /// Example showing how to use the NormalDist class.
  /// </summary>
  class NormalDistExample
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main(string[] args)
    {
      Console.WriteLine();

      // Constructs a NormalDist instance with mean of ten and variance of five.
      NormalDist dist = new NormalDist( 10.0, 5.0 );

      // The PDF() method computes the probability density function
      // evaluated at a given value. The probability of
      // observing a value of 8.7 is:
      Console.WriteLine( "PDF: " +  dist.PDF( 8.7 ));

      // The CDF() method computes the cumulative density function
      // evaluated at a given value. Find the probability of
      // observing 9.2 or less.
      Console.WriteLine( "CDF: " + dist.CDF( 9.2 ));

      // The first four moments of the normal distribution.
      Console.WriteLine( "Mean: " + dist.Mean );
      Console.WriteLine( "Variance: " + dist.Variance );
      Console.WriteLine( "Skewness: " + dist.Skewness );
      Console.WriteLine( "Kurtosis: " + dist.Kurtosis );
      Console.WriteLine();

      Console.WriteLine();
      Console.WriteLine( "Press Enter Key" );
      Console.Read();

    }  // Main

  }  // class

}  // namespace
