# NetStructs

## Purpose

NetStructs provides C# structures to represent various entities commonly used in networking, most notably IP addresses and ports. These structures are designed to be blittable with the data structures used in low-level operating system networking calls (ie, the Linux socket interface and WinSock interface). This library is primarily useful for implementing high-performance networking code that can make lots of assumptions about the transport in use and the architecture of the machine running the application. It is being used to create wrappers around the Myricom DBL and SolarFlare OpenOnload kernel bypass APIs.

Main design goals are:

* Allow conversion of low-level networking data structures to these structures with minimal overhead.
* Allow conversion of NetStructs structures to low-level networking data structures with minimal overhead.
* Allow passing of endpoint data between levels of the network stack without creating allocations.
* Provide syntactic sugar, conversion to BCL types, and convenience methods with reasonable overhead.

Some non-design goals:

* Compatibility with big endian architectures.
* Support for protocols other than IP, UDP, and TCP.
* Support for IPv6 and IPv4 code using the same data structures.
* Drop-in compatibility with built-in BCL networking code (though conversions to BCL types are provided).

## Maturity

NetStructs is currently alpha quality and has no IPv6 support.

## Implementation

IP Addresses and ports are transmitted on the wire in big-endian format. The critical path in high-performance network software tends not to need to do much inspection of the innards of addresses or ports. They simply need to be put onto the wire, or stored for a time prior to being put back on the wire. Looking at the innards to convert to or from human readable formats tend to be done only either at program startup, or selectively to a small sample of overall traffic for monitoring purposes. Therefore, for best performance it makes sense to primary represent endpoint data in big-endian format, and only convert to host format in the rare cases that are required. This is how NetStructs does it. 

Additionally, in C# it's best that performance-critical paths do not generate allocations. NetStucts data structures are mutable structs, maximizing the ability of networking code authors to avoid allocations in their hot paths.

## License

This software is licensed under the MIT License. You can find the full text of the license in the LICENSE.txt file and online at https://opensource.org/licenses/MIT. The license text prevails over this summary, but that generally means you can do whatever you want with it, but if you do use it, you can't sue the authors over any damages it may cause you or others.


## Contributions

Contributions are encouraged, you can submit them via github pull request to the repo at https://www.github.com/scottstephens/NetStructs.



