
using UnityEngine;


public class SphericalCoordinates 
{
	// PUBLIC VARIABLES
	public bool loopPolar = true;
	public bool loopElevation = true;

	// PRIVATE CONST VALUES
	private const float  COMMONSPHERE_2mPI = 6.2831853f;
	private const float  COMMONSPHERE_PId3 = 1.0471976f;

	// PRIVATE VARIABLES
	private float _radius, _minRadius, _maxRadius;
	private float _polar, _minPolar, _maxPolar;
	private float _elevation, _minElevation, _maxElevation;
	
	public SphericalCoordinates(float r, float p, float e,
	                             float minRadius = 0.0f, float maxRadius = 100.0f,
	                             float minPolar = 0.0f, float maxPolar = -COMMONSPHERE_2mPI,
	                             float minElevation = -COMMONSPHERE_PId3, float maxElevation = COMMONSPHERE_PId3)
	{
		_minRadius = minRadius;
		_maxRadius = maxRadius;
		_minPolar = minPolar;
		_maxPolar = maxPolar;
		_minElevation = minElevation;
		_maxElevation = maxElevation;
		
		radius = r;
		polar = p;
		elevation = e;
	}

	public SphericalCoordinates(Vector3 cartesianCoordinate,
	                            float minRadius = 0.0f, float maxRadius = 100.0f,
	                            float minPolar = 0.0f, float maxPolar = -COMMONSPHERE_2mPI,
	                            float minElevation = -COMMONSPHERE_PId3, float maxElevation = COMMONSPHERE_PId3)
	{
		_minRadius = minRadius;
		_maxRadius = maxRadius;
		_minPolar = minPolar;
		_maxPolar = maxPolar;
		_minElevation = minElevation;
		_maxElevation = maxElevation;
		
		FromCartesian (cartesianCoordinate);
	}
	
	public float radius {
		get{ return _radius;}
		set {
			_radius = Mathf.Clamp (value, _minRadius, _maxRadius);
		}
	}
	public float polar
	{
		get{ return _polar;}
		set{
			if(loopPolar)
				_polar = Mathf.Repeat(value,_maxPolar - _minPolar);
			else
				_polar = Mathf.Clamp(value, _minPolar, _maxPolar);
		}
	}
	public float elevation
	{
		get{ return _elevation;}
		set{
			if(loopElevation)
			{
				if(value < _minElevation)
					_elevation = _maxElevation - (value - _minElevation);
				else if( value > _maxElevation)
					_elevation = _minElevation + (value - _maxElevation);
				else
					_elevation = value;
			}
			else
				_elevation = Mathf.Clamp(value, _minElevation, _maxElevation);
		}
	}

	public Vector3 toCartesian
	{
		get{
			float tmp = radius * Mathf.Cos(elevation); // cuts down on math operations
			// Calculate Vector3 (X,Z,Y), Cartesian standard assumes Zenith is from a fix Z rather than Y.
			return new Vector3(tmp * Mathf.Cos(polar), radius * Mathf.Sin(elevation),tmp * Mathf.Sin(polar));
		}
	}
			                   
	public SphericalCoordinates FromCartesian(Vector3 cartesianCoordinate)
	{
		if( cartesianCoordinate.x == 0f )
			cartesianCoordinate.x = Mathf.Epsilon; //Alows use of Atan
		radius = cartesianCoordinate.magnitude;
		
		polar = Mathf.Atan(cartesianCoordinate.z / cartesianCoordinate.x);
		
		if( cartesianCoordinate.x < 0f )
			polar += Mathf.PI;
		elevation = Mathf.Asin(cartesianCoordinate.y / radius);
		
		return this;
	}

}